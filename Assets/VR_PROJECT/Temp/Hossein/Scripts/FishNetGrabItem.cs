using System;
using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using VR_PROJECT;

public class FishNetGrabItem : NetworkBehaviour
{
    private XRGrabInteractable _xrGrabInteractable;

    [FormerlySerializedAs("_isSelected")] [AllowMutableSyncType] [SerializeField]
    private SyncVar<SyncSelectData> _selectData = new SyncVar<SyncSelectData>();

    // Start is called before the first frame update
    private void Awake()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();

        _xrGrabInteractable.selectEntered.AddListener(SelectEnterEvent);
        _xrGrabInteractable.selectExited.AddListener(OnSelectExitEvent);
    }
    
    public void SelectEnterEvent(SelectEnterEventArgs args)
    {
        SerIsSelected(ClientManager.Connection.ClientId,true);
    }

    public void OnSelectExitEvent(SelectExitEventArgs arg)
    {
        SerIsSelected(ClientManager.Connection.ClientId, false);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SerIsSelected(int ownerId, bool isSelected) => _selectData.Value = new SyncSelectData(ownerId, isSelected);

    private void Update()
    {
        if (_selectData.Value is null)
            return;

        if (_selectData.Value._ownerId == ClientManager.Connection.ClientId)
            return;

        if (_selectData.Value._isSelected == _xrGrabInteractable.enabled)
            _xrGrabInteractable.enabled = !_selectData.Value._isSelected;
    }
}

[Serializable]
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