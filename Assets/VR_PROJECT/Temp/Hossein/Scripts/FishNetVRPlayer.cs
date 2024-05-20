using System;
using FishNet.Object;
using RootMotion.FinalIK;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using VR_PROJECT;
using VR_PROJECT.Inputs;

public class FishNetVRPlayer : NetworkBehaviour
{
    [SerializeField] private Renderer _meshRenderer;
    [SerializeField] private TMP_Text _playerIdText;

    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _headTargetIK;
    [SerializeField] private Transform _handLTargetIK;
    [SerializeField] private Transform _handRTargetIK;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintSpeedOffset;

    private XRInputModalityManager _xrInputModalityManager;
    private IKTransforms _nextTransform;
    private DynamicMoveProvider _moveProvider;

    private Transform _camera;
    private Transform _handRAnchor;
    private Transform _handLAnchor;
    private float _canvasOffset;
    private float _lerpSpeed;

    private IInputManager _inputManager;

    private void Awake()
    {
        // Find necessary components in the scene
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();
        _moveProvider = FindObjectOfType<DynamicMoveProvider>();
        _inputManager = GameManager.Instance.InputManager;

        // Initialize movement speed and IK transform container
        _moveSpeed = _moveProvider.moveSpeed;
        _nextTransform = new IKTransforms();
        
        // Get the main camera's transform
        _camera = Camera.main.transform;

        // Set default sprint speed offset and canvas offset values
        _sprintSpeedOffset = 4f;
        _canvasOffset = 0.5f;
        _lerpSpeed = 25f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Display the player's network ID
        _playerIdText.text = ObjectId.ToString();

        // Hide the canvas and mesh for the local player
        _canvas.gameObject.SetActive(!IsOwner);
        _meshRenderer.enabled = !IsOwner;
    }

    // Update is called once per frame
    void Update()
    {
        // Skip if the client is not initialized
        if (!IsClientInitialized)
            return;

        if (IsOwner) // Local player
        {
            SetMoveSpeed(); // Set movement speed based on input
            var transform = GetLocalTransforms(); // Get the local player's transform
            SetLocalTransforms(transform); // Apply the local player's transform
            SendNextTransforms(transform); // Send the local player's transform to the server
        }
        else // Remote players
        {
            LerpToNextTransform(); // Smoothly update remote player's transform
        }
    }

    private void SetMoveSpeed()
    {
        // Adjust movement speed based on whether the player is sprinting
        _moveProvider.moveSpeed = _inputManager.Sprint ? _moveSpeed * _sprintSpeedOffset : _moveSpeed;
    }

    [ServerRpc(RequireOwnership = true)] // Check if it is owner(Local Player) can only send data 
    // Send the local player's transform data to the server
    private void SendNextTransforms(IKTransforms playerTransform)
    {
        SetNextTransforms(playerTransform, ObjectId);
    }

    [ObserversRpc(ExcludeOwner = true)]
    // Update remote players with the transform data received from the server
    private void SetNextTransforms(IKTransforms playerTransform, int objectId)
    {
        // Check if the received object ID matches this object's ID
        if (ObjectId != objectId)
            return;

        // Update the next transform to interpolate towards
        _nextTransform = playerTransform;
    }

    // Smoothly interpolate the transform towards the target position and rotation
    private void LerpToNextTransform()
    {
        // If there is no next transform data, skip
        if (_nextTransform is null)
            return;

        // Smoothly interpolate the head position and rotation
        _headTargetIK.position = Vector3.Lerp(_headTargetIK.position, _nextTransform.HeadPosition, _lerpSpeed * Time.deltaTime);
        _headTargetIK.rotation = Quaternion.Lerp(_headTargetIK.rotation, _nextTransform.HeadRotation, _lerpSpeed * Time.deltaTime);

        // Smoothly interpolate the left hand position and rotation
        _handLTargetIK.position = Vector3.Lerp(_handLTargetIK.position, _nextTransform.HandLPosition, _lerpSpeed * Time.deltaTime);
        _handLTargetIK.rotation = Quaternion.Lerp(_handLTargetIK.rotation, _nextTransform.HandLRotation, _lerpSpeed * Time.deltaTime);

        // Smoothly interpolate the right hand position and rotation
        _handRTargetIK.position = Vector3.Lerp(_handRTargetIK.position, _nextTransform.HandRPosition, _lerpSpeed * Time.deltaTime);
        _handRTargetIK.rotation = Quaternion.Lerp(_handRTargetIK.rotation, _nextTransform.HandRRotation, _lerpSpeed * Time.deltaTime);

        // Smoothly interpolate the canvas position
        _canvas.position = Vector3.Lerp(_canvas.position, _nextTransform.CanvasPosition, _lerpSpeed * Time.deltaTime);
    }

    // Get the transforms for the head, hands, and canvas based on the current XR input mode
    private IKTransforms GetLocalTransforms()
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
            default:
                throw new ArgumentOutOfRangeException();
        }

        // Get the head position and rotation from the main camera
        var nextHeadPosition = _camera.transform.position;
        var nextHeadRotation = _camera.transform.rotation;

        // Set the head position and rotation
        transform.HeadPosition = nextHeadPosition;
        transform.HeadRotation = nextHeadRotation;

        // Calculate the canvas position based on the head position
        var nextCanvasPosition = new Vector3(nextHeadPosition.x, nextHeadPosition.y + _canvasOffset, nextHeadPosition.z);
        // Set the canvas position
        transform.CanvasPosition = nextCanvasPosition;

        // Return the filled IKTransforms object
        return transform;
    }

    // Apply the local transforms directly to the FinalIK target transforms
    private void SetLocalTransforms(IKTransforms transformData)
    {
        // If the transform data is null, skip
        if (transformData is null)
            return;

        // Set the head position and rotation from the transform data
        _headTargetIK.position = transformData.HeadPosition;
        _headTargetIK.rotation = transformData.HeadRotation;

        // Set the right hand position and rotation from the transform data
        _handRTargetIK.position = transformData.HandRPosition;
        _handRTargetIK.rotation = transformData.HandRRotation;

        // Set the left hand position and rotation from the transform data
        _handLTargetIK.position = transformData.HandLPosition;
        _handLTargetIK.rotation = transformData.HandLRotation;

        // Set the canvas position from the transform data
        _canvas.position = transformData.CanvasPosition;
    }
}