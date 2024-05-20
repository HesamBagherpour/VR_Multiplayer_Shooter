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
        _xrInputModalityManager = FindObjectOfType<XRInputModalityManager>();
        _moveProvider = FindObjectOfType<DynamicMoveProvider>();
        _inputManager = GameManager.Instance.InputManager;

        _moveSpeed = _moveProvider.moveSpeed;
        _nextTransform = new IKTransforms();
        _camera = Camera.main.transform;
        _sprintSpeedOffset = 4f;
        _canvasOffset = 0.5f;
        _lerpSpeed = 25f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        _playerIdText.text = ObjectId.ToString();

        _canvas.gameObject.SetActive(!IsOwner);
        _meshRenderer.enabled = !IsOwner;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsClientInitialized) // before clint init 
            return;

        if (IsOwner) // local player 
        {
            SetMoveSpeed();
            var transform = GetLocalTransforms();
            SetLocalTransforms(transform); // set player transform || position and rotation 
            SendNextTransforms(transform);  // send player transform data to server and other clint 
        }
        else // remote players
        {
            
            LerpToNextTransform();
        }
    }

    private void SetMoveSpeed()
    {
        _moveProvider.moveSpeed = _inputManager.Sprint ? _moveSpeed * _sprintSpeedOffset : _moveSpeed;
    }

    [ServerRpc(RequireOwnership = true)] 
    // Check if owner ( Player )  can only send data 
    // This function only owner send data to server 
    // This methode all in server side
     private  void SendNextTransforms(IKTransforms playerTransform)
    {
       SetNextTransforms(playerTransform, ObjectId);
    }
    // This method run in client side only 
    // all the players receive  data  except  local player 
    [ObserversRpc(ExcludeOwner = true)] 
    private void SetNextTransforms(IKTransforms playerTransform, int objectId)
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



        _headTargetIK.position = Vector3.Lerp(_headTargetIK.position, _nextTransform.HeadPosition,
            _lerpSpeed * Time.deltaTime);
        _headTargetIK.rotation = Quaternion.Lerp(_headTargetIK.rotation, _nextTransform.HeadRotation,
            _lerpSpeed * Time.deltaTime);

        
        _handLTargetIK.position = Vector3.Lerp(_handLTargetIK.position, _nextTransform.HandLPosition,
            _lerpSpeed * Time.deltaTime);
        _handLTargetIK.rotation = Quaternion.Lerp(_handLTargetIK.rotation, _nextTransform.HandLRotation,
            _lerpSpeed * Time.deltaTime);


        _handRTargetIK.position = Vector3.Lerp(_handRTargetIK.position, _nextTransform.HandRPosition,
            _lerpSpeed * Time.deltaTime);
        _handRTargetIK.rotation = Quaternion.Lerp(_handRTargetIK.rotation, _nextTransform.HandRRotation,
            _lerpSpeed * Time.deltaTime);

        
        _canvas.position = Vector3.Lerp(_canvas.position, _nextTransform.CanvasPosition,
            _lerpSpeed * Time.deltaTime);
    }


    
    // This function is  get XR transform 

    // check if it is TrackedHand or MotionController 
    
    private IKTransforms GetLocalTransforms()
    {
        var transform = new IKTransforms();

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

        transform.HeadPosition = nextHeadPosition;
        transform.HeadRotation = nextHeadRotation;

        
        
        var nextCanvasPosition =
            new Vector3(nextHeadPosition.x, nextHeadPosition.y + _canvasOffset, nextHeadPosition.z);


        transform.CanvasPosition = nextCanvasPosition;

        return transform;
    }

    
    
    
    
    // This function set transform  from XR  into  FinalIK  Target  Transform
    
    
    //TODO
    //must be change
    //if it is local player set data directly
    // create separate  class to make remote player and local player 

    
    private void SetLocalTransforms(IKTransforms transformData)
    {
        if (transformData is null)
            return;


        _headTargetIK.position =transformData.HeadPosition;
        _headTargetIK.rotation =transformData.HeadRotation;



        _handRTargetIK.position = transformData.HandRPosition;
        _handRTargetIK.rotation = transformData.HandRRotation;



        _handLTargetIK.position = transformData.HandLPosition;
        _handLTargetIK.rotation = transformData.HandLRotation;


        _canvas.position = transformData.CanvasPosition;
    }
}