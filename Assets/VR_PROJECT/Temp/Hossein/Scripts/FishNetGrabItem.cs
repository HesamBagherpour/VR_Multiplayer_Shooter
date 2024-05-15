using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FishNetGrabItem : NetworkBehaviour
{
    private XRGrabInteractable _xrGrabInteractable;

    private readonly SyncVar<SyncSelectData> _isSelected = new SyncVar<SyncSelectData>();

    // Start is called before the first frame update
    private void Awake()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();

        _isSelected.OnChange += OnSelectChange;

        _xrGrabInteractable.selectEntered.AddListener((arg) =>
            _isSelected.Value = new SyncSelectData(ObjectId, true));
        
        _xrGrabInteractable.selectExited.AddListener((arg) => 
            _isSelected.Value = new SyncSelectData(ObjectId, false));
    }

    private void OnSelectChange(SyncSelectData prev, SyncSelectData next, bool asserver)
    {
        if (next == null || next._ownerId == ObjectId)
            return;

        _xrGrabInteractable.enabled = !next._isSelected;
    }
}

public class SyncSelectData
{
    public int _ownerId;
    public bool _isSelected;

    public SyncSelectData()
    {
        _ownerId = -1;
        _isSelected = false;
    }

    public SyncSelectData(int ownerId, bool isSelected)
    {
        _ownerId = ownerId;
        _isSelected = isSelected;
    }
}