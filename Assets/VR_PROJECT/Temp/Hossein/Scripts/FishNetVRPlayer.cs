using System;
using FishNet.Object;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

[RequireComponent(typeof(VRIK))]
public class FishNetVRPlayer : NetworkBehaviour
{
    private VRIK _vrik;
    private XRInputModalityManager _xrInputModalityManager;

    private Transform _camera;

    private Transform _headTargetIK;
    private Transform _handLTargetIK;
    private Transform _handRTargetIK;


    private Transform _handRAnchor;
    private Transform _handLAnchor;
    private float _canvasOffset;


    private void Awake()
    {
        _vrik = GetComponent<VRIK>();
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        // _headTargetVR = GameObject.FindGameObjectWithTag("VRHeadTarget").transform;
        //
        // _handLTargetVR = GameObject.FindGameObjectWithTag("VRHandLTarget").transform;
        //
        // _handRTargetVR = GameObject.FindGameObjectWithTag("VRHandRTarget").transform;


        if (IsOwner)
        {
            // _vrik.solver.spine.headTarget = _headTargetVR;
            // _vrik.solver.leftArm.target = _handLTargetVR;
            // _vrik.solver.rightArm.target = _handRTargetVR;
        }
        else
        {
            _headTargetIK = new GameObject("HeadTarget").transform;
            _vrik.solver.spine.headTarget = _headTargetIK;

            _handLTargetIK = new GameObject("HandLTarget").transform;
            _vrik.solver.leftArm.target = _handLTargetIK;

            _handRTargetIK = new GameObject("HandRTarget").transform;
            _vrik.solver.rightArm.target = _handRTargetIK;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var transform = GetLocalTransforms();

        SetTransforms(transform);
    }

    private PlayerTransform GetLocalTransforms()
    {
        var transform = new PlayerTransform();

        switch (XRInputModalityManager.currentInputMode.Value)
        {
            case XRInputModalityManager.InputMode.None:
                break;
            case XRInputModalityManager.InputMode.TrackedHand:

                if (_handRAnchor is null)
                    _handRAnchor = GameObject.FindGameObjectWithTag("RHandAnchor").transform;

                transform.HandRPosition = _handRAnchor.position;
                transform.HandRRotation = _handRAnchor.rotation;

                if (_handLAnchor is null)
                    _handLAnchor = GameObject.FindGameObjectWithTag("LHandAnchor").transform;

                transform.HandLPosition = _handLAnchor.position;
                transform.HandLRotation = _handLAnchor.rotation;

                break;
            case XRInputModalityManager.InputMode.MotionController:

                transform.HandRPosition = _xrInputModalityManager.rightController.transform.position;
                transform.HandRRotation = _xrInputModalityManager.rightController.transform.rotation;

                transform.HandLPosition = _xrInputModalityManager.leftController.transform.position;
                transform.HandLRotation = _xrInputModalityManager.leftController.transform.rotation;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var nextHeadPosition = _camera.transform.position;
        var nextHeadRotation = _camera.transform.rotation;

        var nextCanvasPosition =
            new Vector3(nextHeadPosition.x, nextHeadPosition.y + _canvasOffset, nextHeadPosition.z);

        transform.HeadPosition = nextHeadPosition;
        transform.HeadRotation = nextHeadRotation;

        transform.CanvasPosition = nextCanvasPosition;

        return transform;
    }

    private void SetTransforms(PlayerTransform playerTransform)
    {
        if (IsOwner)
        {
            _headTargetIK.position = playerTransform.HeadPosition;
            _headTargetIK.rotation = playerTransform.HeadRotation;

            _handRTargetIK.position = playerTransform.HandRPosition;
            _handRTargetIK.rotation = playerTransform.HandRRotation;

            _handLTargetIK.position = playerTransform.HandLPosition;
            _handLTargetIK.rotation = playerTransform.HandLRotation;
        }
        else
        {
        }
    }
}