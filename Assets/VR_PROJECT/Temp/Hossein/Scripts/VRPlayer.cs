using TMPro;
using UnityEngine;

public abstract class VRPlayer : MonoBehaviour
{
    protected IKTransforms _nextTransform;
    protected FishNetVRPlayer _fishNetVRPlayer;
    
    public Renderer _meshRenderer;
    public TMP_Text _playerIdText;
    protected Transform _headTargetIK;
    protected Transform _handRTargetIK;
    protected Transform _handLTargetIK;
    protected Transform _canvas;
    protected Transform _camera;
    
    protected float _canvasOffset;
    
    public IKTransforms NextTransform => _nextTransform;

    private void Awake()
    {
        Init();
    }
    
    protected virtual void Init()
    {
        _fishNetVRPlayer = GetComponent<FishNetVRPlayer>();
        var data = GetComponent<VRPlayerRefrences>();
        
        _meshRenderer = data.meshRenderer;
        _playerIdText = data.playerIdText;
        _headTargetIK = data.headTargetIK;
        _handRTargetIK = data.handRTargetIK;
        _handLTargetIK = data.handLTargetIK;
        _canvas = data.canvas;
        
        // Display the player's network ID
        _playerIdText.text = _fishNetVRPlayer.ObjectId.ToString();

        // Hide the canvas and mesh for the local player
        _canvas.gameObject.SetActive(!_fishNetVRPlayer.IsOwner);
        _meshRenderer.enabled = !_fishNetVRPlayer.IsOwner;

        // Initialize movement speed and IK transform container
        _nextTransform = new IKTransforms();
        
        // Get the main camera's transform
        _camera = Camera.main.transform;

        // Set default sprint speed offset and canvas offset values
        _canvasOffset = 0.5f;
    }
    public abstract void SetTransform(IKTransforms nextTransform);
}