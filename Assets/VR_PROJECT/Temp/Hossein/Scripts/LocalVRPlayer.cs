using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using VR_PROJECT;
using VR_PROJECT.Inputs;

public class LocalVRPlayer : VRPlayer
{
    private XRInputModalityManager _xrInputModalityManager;
    private DynamicMoveProvider _moveProvider;
    private IInputManager _inputManager;

    private Transform _handRAnchor;
    private Transform _handLAnchor;
    
    protected float _moveSpeed;
    protected float _sprintSpeedOffset;

    protected override void Init()
    {
        base.Init();

        // Find necessary components in the scene
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();
        _moveProvider = FindObjectOfType<DynamicMoveProvider>();
        _inputManager = GameManager.Instance.InputManager;

        _moveSpeed = _moveProvider.moveSpeed;
        _sprintSpeedOffset = 4f;
    }

    private void Update()
    {
        SetMoveSpeed();// Set movement speed based on input
        _nextTransform = GetTransforms(); // Get the local player's transform
        SetTransform(_nextTransform); // Apply the local player's transform
    }
    
    // Apply the local transforms directly to the FinalIK target transforms
    public override void SetTransform(IKTransforms nextTransform)
    {
        // If the transform data is null, skip
        if (nextTransform is null)
            return;

        // Set the head position and rotation from the transform data
        _headTargetIK.position = nextTransform.HeadPosition;
        _headTargetIK.rotation = nextTransform.HeadRotation;

        // Set the right hand position and rotation from the transform data
        _handRTargetIK.position = nextTransform.HandRPosition;
        _handRTargetIK.rotation = nextTransform.HandRRotation;

        // Set the left hand position and rotation from the transform data
        _handLTargetIK.position = nextTransform.HandLPosition;
        _handLTargetIK.rotation = nextTransform.HandLRotation;

        // Set the canvas position from the transform data
        _canvas.position = nextTransform.CanvasPosition;
    }
    
    // Get the transforms for the head, hands, and canvas based on the current XR input mode
    private IKTransforms GetTransforms()
    {
        // Create a new IKTransforms object to store transform data
        var transform = new IKTransforms();

        // Check the current input mode of the XR device
        switch (XRInputModalityManager.currentInputMode.Value)
        {
            case XRInputModalityManager.InputMode.None:
                break;
            case XRInputModalityManager.InputMode.TrackedHand:
                // If the right hand anchor is not set, find it in the scene
                if (_handRAnchor is null)
                    _handRAnchor = GameObject.FindGameObjectWithTag("RHandAnchor").transform;

                // Get the right hand position and rotation
                transform.HandRPosition = _handRAnchor.position;
                transform.HandRRotation = _handRAnchor.rotation;

                // If the left hand anchor is not set, find it in the scene
                if (_handLAnchor is null)
                    _handLAnchor = GameObject.FindGameObjectWithTag("LHandAnchor").transform;

                // Get the left hand position and rotation
                transform.HandLPosition = _handLAnchor.position;
                transform.HandLRotation = _handLAnchor.rotation;
                break;
            case XRInputModalityManager.InputMode.MotionController:
                // Get the right controller position and rotation
                transform.HandRPosition = _xrInputModalityManager.rightController.transform.position;
                transform.HandRRotation = _xrInputModalityManager.rightController.transform.rotation;

                // Get the left controller position and rotation
                transform.HandLPosition = _xrInputModalityManager.leftController.transform.position;
                transform.HandLRotation = _xrInputModalityManager.leftController.transform.rotation;
                break;
        }

        // Get the head position and rotation from the main camera
        var nextHeadPosition = _camera.transform.position;
        var nextHeadRotation = _camera.transform.rotation;

        // Set the head position and rotation
        transform.HeadPosition = nextHeadPosition;
        transform.HeadRotation = nextHeadRotation;

        // Calculate the canvas position based on the head position
        var nextCanvasPosition =
            new Vector3(nextHeadPosition.x, nextHeadPosition.y + _canvasOffset, nextHeadPosition.z);
        // Set the canvas position
        transform.CanvasPosition = nextCanvasPosition;

        // Return the filled IKTransforms object
        return transform;
    }  
    
    private void SetMoveSpeed()
    {
        // Adjust movement speed based on whether the player is sprinting
        _moveProvider.moveSpeed = _inputManager.Sprint ? _moveSpeed * _sprintSpeedOffset : _moveSpeed;
    }
}