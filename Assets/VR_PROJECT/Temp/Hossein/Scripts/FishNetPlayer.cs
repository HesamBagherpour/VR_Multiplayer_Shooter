using FishNet.Object;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class FishNetPlayer : NetworkBehaviour
{
    [SerializeField] private Transform _canvas;
    [SerializeField] private TMP_Text _playerIdText;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _handRTransform;
    [SerializeField] private Transform _handLTransform;

    private XRInputModalityManager _xrInputModalityManager;
    private Camera _camera;
    private Transform _handRAnchor;
    private Transform _handLAnchor;
    private PlayerTransform _nextTransforms;
    private float _headOffset;
    private float _lerpSpeed;
    private float _canvasOffset;

    private void Awake()
    {
        _camera = Camera.main;
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();
        var leftHand = _xrInputModalityManager.leftHand;

        _headOffset = 0.4f;
        _canvasOffset = 0.5f;
        _lerpSpeed = 25f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        _playerIdText.text = ObjectId.ToString();

        if (!IsOwner)
            return;

        _canvas.gameObject.SetActive(false);

        _headTransform.gameObject.SetActive(false);
        _handRTransform.gameObject.SetActive(false);
        _handLTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        var nextTransform = CalculateNextTransform();

        SendNextTransforms(nextTransform);

        LerpToNextTransform();
    }

    private PlayerTransform CalculateNextTransform()
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

        var nextHeadPosition = headPosition + (-_camera.transform.forward * _headOffset);
        var nextCanvasPosition =
            new Vector3(nextHeadPosition.x, nextHeadPosition.y + _canvasOffset, nextHeadPosition.z);

        var transform = new PlayerTransform()
        {
            // CanvasPosition = nextCanvasPosition,
            // HeadPosition = nextHeadPosition,
            // HeadRotation = headRotation,
            // HandLPosition = handLPosition,
            // HandLRotation = handLRotation,
            // HandRPosition = handRPosition,
            // HandRRotation = handRRotation
        };

        return transform;
    }

    [ServerRpc(RequireOwnership = true)]
    public void SendNextTransforms(PlayerTransform transform)
    {
        SetNextTransforms(transform, ObjectId);
    }

    [ObserversRpc(ExcludeOwner = true)]
    public void SetNextTransforms(PlayerTransform transform, int objectId)
    {
        if (ObjectId != objectId)
            return;

        _nextTransforms = transform;
    }

    // Lerp towards the target position and rotation
    public void LerpToNextTransform()
    {
        if (_nextTransforms is null)
            return;

        // _canvas.position = Vector3.Lerp(_canvas.position, _nextTransforms.CanvasPosition,
        //     _lerpSpeed * Time.deltaTime);
        //
        // _headTransform.position = Vector3.Lerp(_headTransform.position, _nextTransforms.HeadPosition,
        //     _lerpSpeed * Time.deltaTime);
        // _headTransform.rotation = Quaternion.Lerp(_headTransform.rotation, _nextTransforms.HeadRotation,
        //     _lerpSpeed * Time.deltaTime);
        //
        // _handLTransform.position = Vector3.Lerp(_handLTransform.position, _nextTransforms.HandLPosition,
        //     _lerpSpeed * Time.deltaTime);
        // _handLTransform.rotation = Quaternion.Lerp(_handLTransform.rotation, _nextTransforms.HandLRotation,
        //     _lerpSpeed * Time.deltaTime);
        //
        // _handRTransform.position = Vector3.Lerp(_handRTransform.position, _nextTransforms.HandRPosition,
        //     _lerpSpeed * Time.deltaTime);
        // _handRTransform.rotation = Quaternion.Lerp(_handRTransform.rotation, _nextTransforms.HandRRotation,
        //     _lerpSpeed * Time.deltaTime);
    }
}

// public class PlayerTransform
// {
//     public Vector3 CanvasPosition;
//     
//     public Vector3 HeadPosition;
//     public Quaternion HeadRotation;
//
//     public Vector3 HandRPosition;
//     public Quaternion HandRRotation;
//
//     public Vector3 HandLPosition;
//     public Quaternion HandLRotation;
// }