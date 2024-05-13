using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class FishNetPlayer : NetworkBehaviour
{
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _handRTransform;
    [SerializeField] private Transform _handLTransform;

    private XRInputModalityManager _xrInputModalityManager;
    private Camera _camera;
    private Transform _handRAnchor;
    private Transform _handLAnchor;


    private void Awake()
    {
        _camera = Camera.main;
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();
        var leftHand = _xrInputModalityManager.leftHand;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
            return;

        _headTransform.gameObject.SetActive(false);
        _handRTransform.gameObject.SetActive(false);
        _handLTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 headPosition = _camera.transform.position;
        Quaternion headRotation = _camera.transform.rotation;

        Vector3 handRPosition = _handRTransform.position;
        Quaternion handRRotation = _handRTransform.rotation;

        Vector3 handLPosition = _handLTransform.position;
        Quaternion handLRotation = _handLTransform.rotation;

        switch (XRInputModalityManager.currentInputMode.Value)
        {
            case XRInputModalityManager.InputMode.None:
                break;
            case XRInputModalityManager.InputMode.TrackedHand:
                
                if (_handRAnchor is null)
                    _handRAnchor = GameObject.FindGameObjectWithTag("RHandAnchor").transform;
                
                handRPosition = _handRAnchor.position;
                handRRotation = _handRAnchor.rotation;
                
                if (_handLAnchor is null)
                    _handLAnchor = GameObject.FindGameObjectWithTag("LHandAnchor").transform;
                
                handLPosition = _handLAnchor.position;
                handLRotation = _handLAnchor.rotation;

                break;
            case XRInputModalityManager.InputMode.MotionController:

                handRPosition = _xrInputModalityManager.rightController.transform.position;
                handRRotation = _xrInputModalityManager.rightController.transform.rotation;

                handLPosition = _xrInputModalityManager.leftController.transform.position;
                handLRotation = _xrInputModalityManager.leftController.transform.rotation;

                break;
        }

        var transform = new PlayerTransform()
        {
            HeadPosition = headPosition,
            HeadRotation = headRotation,
            HandLPosition = handLPosition,
            HandLRotation = handLRotation,
            HandRPosition = handRPosition,
            HandRRotation = handRRotation
        };

        SendPostionsAndRotations(transform, (-_camera.transform.forward * .4f));
    }

    [ServerRpc(RequireOwnership = true)]
    public void SendPostionsAndRotations(PlayerTransform transform, Vector3 headOffset)
    {
        SetPostionsAndRotations(transform, headOffset);
    }

    [ObserversRpc(ExcludeOwner = true)]
    public void SetPostionsAndRotations(PlayerTransform transform, Vector3 headOffset)
    {
        _headTransform.transform.position = transform.HeadPosition + headOffset;
        _headTransform.transform.rotation = transform.HeadRotation;

        _handLTransform.transform.position = transform.HandLPosition;
        _handLTransform.transform.rotation = transform.HandLRotation;


        _handRTransform.transform.position = transform.HandRPosition;
        _handRTransform.transform.rotation = transform.HandRRotation;
    }
}

public class PlayerTransform
{
    public Vector3 HeadPosition;
    public Quaternion HeadRotation;

    public Vector3 HandRPosition;
    public Quaternion HandRRotation;

    public Vector3 HandLPosition;
    public Quaternion HandLRotation;
}