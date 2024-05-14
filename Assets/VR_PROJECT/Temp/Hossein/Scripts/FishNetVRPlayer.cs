using System;
using System.Collections.Generic;
using FishNet.Object;
using RootMotion.FinalIK;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

[RequireComponent(typeof(VRIK))]
public class FishNetVRPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _playerIdText;

    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _headTargetIK;
    [SerializeField] private Transform _handLTargetIK;
    [SerializeField] private Transform _handRTargetIK;


    private VRIK _vrik;
    private XRInputModalityManager _xrInputModalityManager;
    private PlayerTransformData _nextTransform;

    private Transform _camera;
    private Transform _handRAnchor;
    private Transform _handLAnchor;
    private float _canvasOffset;
    private float _lerpSpeed;

    private void Awake()
    {
        _vrik = GetComponent<VRIK>();
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();

        _nextTransform = new PlayerTransformData();
        _camera = Camera.main.transform;
        _canvasOffset = 0.5f;
        _lerpSpeed = 20f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        _playerIdText.text = ObjectId.ToString();

        if (!IsOwner)
            return;

        _canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
     void Update()
    {
        if(!IsClientInitialized)
            return;
        
        if (IsOwner)
        {
            var transform = GetLocalTransforms();
            SetLocalTransforms(transform);
            SendNextTransforms(transform);
        }
        else
        {
            LerpToNextTransform();
        }
    }

    [ServerRpc(RequireOwnership = true)]
    public void SendNextTransforms(PlayerTransformData playerTransform)
    {
        SetNextTransforms(playerTransform, ObjectId);
    }

    [ObserversRpc(ExcludeOwner = true)]
    public void SetNextTransforms(PlayerTransformData playerTransform, int objectId)
    {
        if (ObjectId != objectId)
            return;

        _nextTransform = playerTransform;
    }

    // Lerp towards the target position and rotation
    private void LerpToNextTransform()
    {
        if (_nextTransform is null)
            return;

        var headTransform = _nextTransform.Get(PlayerTransformType.Head).Transform;
        
        _headTargetIK.position = Vector3.Lerp(_headTargetIK.position, headTransform.Position,
            _lerpSpeed * Time.deltaTime);
        _headTargetIK.rotation = Quaternion.Lerp(_headTargetIK.rotation, headTransform.Rotation,
            _lerpSpeed * Time.deltaTime);
        
        var handLTransform = _nextTransform.Get(PlayerTransformType.LHand).Transform;

        _handLTargetIK.position = Vector3.Lerp(_handLTargetIK.position, handLTransform.Position,
            _lerpSpeed * Time.deltaTime);
        _handLTargetIK.rotation = Quaternion.Lerp(_handLTargetIK.rotation, handLTransform.Rotation,
            _lerpSpeed * Time.deltaTime);
        
        var handRTransform = _nextTransform.Get(PlayerTransformType.RHand).Transform;
        
        _handRTargetIK.position = Vector3.Lerp(_handRTargetIK.position, handRTransform.Position,
            _lerpSpeed * Time.deltaTime);
        _handRTargetIK.rotation = Quaternion.Lerp(_handRTargetIK.rotation, handRTransform.Rotation,
            _lerpSpeed * Time.deltaTime);

        
        var canvasTransform = _nextTransform.Get(PlayerTransformType.Canvas);

        _canvas.position = Vector3.Lerp(_canvas.position, canvasTransform.Transform.Position,
            _lerpSpeed * Time.deltaTime);
        
    }

    private PlayerTransformData GetLocalTransforms()
    {
        var transform = new PlayerTransformData();

        switch (XRInputModalityManager.currentInputMode.Value)
        {
            case XRInputModalityManager.InputMode.None:
                break;
            case XRInputModalityManager.InputMode.TrackedHand:

                if (_handRAnchor is null)
                    _handRAnchor = GameObject.FindGameObjectWithTag("RHandAnchor").transform;
                
                transform.Set(PlayerTransformType.RHand, _handRAnchor);

                if (_handLAnchor is null)
                    _handLAnchor = GameObject.FindGameObjectWithTag("LHandAnchor").transform;

                transform.Set(PlayerTransformType.LHand, _handLAnchor);

                break;
            case XRInputModalityManager.InputMode.MotionController:

                transform.Set(PlayerTransformType.RHand, _xrInputModalityManager.rightController.transform);
                transform.Set(PlayerTransformType.LHand, _xrInputModalityManager.leftController.transform);

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var nextHeadPosition = _camera.transform.position;
        
        var nextCanvasPosition =
            new Vector3(nextHeadPosition.x, nextHeadPosition.y + _canvasOffset, nextHeadPosition.z);

        transform.Set(PlayerTransformType.Head, _camera.transform);

        var canvasTransform = new PlayerTransform()
            { Type = PlayerTransformType.Canvas, Transform = new CustomTransform() { Position = nextCanvasPosition } };
        
        transform.Set(canvasTransform);

        return transform;
    }

    private void SetLocalTransforms(PlayerTransformData transformData)
    {
        if(transformData is null)
            return;
        
        var headTransform = transformData.Get(PlayerTransformType.Head).Transform;

        _headTargetIK.position = headTransform.Position;
        _headTargetIK.rotation = headTransform.Rotation;
        
        var handRTransform = transformData.Get(PlayerTransformType.RHand).Transform;

        _handRTargetIK.position = handRTransform.Position;
        _handRTargetIK.rotation = handRTransform.Rotation;
        
        var handLTransform = transformData.Get(PlayerTransformType.LHand).Transform;

        _handLTargetIK.position = handLTransform.Position;
        _handLTargetIK.rotation = handLTransform.Rotation;

        var canvasTransform = transformData.Get(PlayerTransformType.Canvas).Transform;

        _canvas.position = canvasTransform.Position;
    }
}