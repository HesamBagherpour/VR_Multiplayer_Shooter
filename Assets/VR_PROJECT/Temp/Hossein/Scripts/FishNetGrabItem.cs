using System;
using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using VR_PROJECT;

public class FishNetGrabItem : NetworkBehaviour
{
    private XRGrabInteractable _xrGrabInteractable;
    private XRGeneralGrabTransformer _xrGrabTransformer;
    private Rigidbody _rigidbody;

    [FormerlySerializedAs("_isSelected")] [AllowMutableSyncType] [SerializeField]
    private SyncVar<SyncSelectData> _selectData = new SyncVar<SyncSelectData>();

    // Start is called before the first frame update
    private void Awake()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        _xrGrabTransformer = GetComponent<XRGeneralGrabTransformer>();
        _rigidbody = GetComponent<Rigidbody>();

        _xrGrabInteractable.selectEntered.AddListener(SelectEnterEvent);
        _xrGrabInteractable.selectExited.AddListener(OnSelectExitEvent);
    }

    public void SelectEnterEvent(SelectEnterEventArgs args)
    {
        SetIsSelected(ClientManager.Connection.ClientId, true);
    }

    public void OnSelectExitEvent(SelectExitEventArgs arg)
    {
        SetIsSelected(-1, false);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetIsSelected(int ownerId, bool isSelected) =>
        _selectData.Value = new SyncSelectData(ownerId, isSelected);

    private void Update()
    {
        if (!IsClientInitialized)
            return;

        ChangeActiveGrab();
        CheckForResetRequest();
    }
    private void ChangeActiveGrab()
    {
        if (_selectData.Value is null)
            return;

        if (_selectData.Value._ownerId == ClientManager.Connection.ClientId)
            return;

        _rigidbody.isKinematic = _selectData.Value._isSelected;
        _xrGrabInteractable.enabled = !_selectData.Value._isSelected;
        _xrGrabTransformer.enabled = !_selectData.Value._isSelected;
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void CheckForResetRequest()
    {
        CheckForReset();
    }

    [ObserversRpc]
    private void CheckForReset()
    {
        if (!_selectData.Value._isSelected)
            return;

        if (!_selectData.Value.IsValid)
            return;

        if (ClientManager.Clients.TryGetValue(_selectData.Value._ownerId, out var connection))
            return;

        SetIsSelected(-1, false);
    }


}

[Serializable]
public class SyncSelectData
{
    public int _ownerId;
    public bool _isSelected;

    public bool IsValid => _ownerId >= 0;

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