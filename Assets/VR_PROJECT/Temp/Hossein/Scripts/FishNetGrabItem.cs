using System;
using FishNet.CodeGenerating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class FishNetGrabItem : NetworkBehaviour
{
    private XRGrabInteractable _xrGrabInteractable;
    private XRGeneralGrabTransformer _xrGrabTransformer;
    private Rigidbody _rigidbody;

    // SyncVar to synchronize selection data across the network
    [FormerlySerializedAs("_isSelected")] 
    [AllowMutableSyncType] 
    [SerializeField]
    private SyncVar<SyncSelectData> _selectData = new SyncVar<SyncSelectData>();

    // Start is called before the first frame update
    private void Awake()
    {
        // Get necessary components from the GameObject
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        _xrGrabTransformer = GetComponent<XRGeneralGrabTransformer>();
        _rigidbody = GetComponent<Rigidbody>();

        // Add event listeners for grab and release actions
        _xrGrabInteractable.selectEntered.AddListener(SelectEnterEvent);
        _xrGrabInteractable.selectExited.AddListener(OnSelectExitEvent);
    }

    // Called when the object is grabbed
    // Method called when the object is grabbed
    public void SelectEnterEvent(SelectEnterEventArgs args)
    {
        // Set the object as selected by the current client using the client ID
        SetIsSelected(ClientManager.Connection.ClientId, true);
    }

    // Method called when the object is released
    public void OnSelectExitEvent(SelectExitEventArgs arg)
    {
        // Set the object as not selected
        SetIsSelected(-1, false);
    }

    // Server-side method to update the selection status
    [ServerRpc(RequireOwnership = false)]
    private void SetIsSelected(int ownerId, bool isSelected) =>
        // Update the selection data with the new owner ID and selection status
        _selectData.Value = new SyncSelectData(ownerId, isSelected);

    // Update is called once per frame
    private void Update()
    {
        // Skip if the client is not initialized
        if (!IsClientInitialized)
            return;

        // Update the grab status if necessary
        ChangeActiveGrab();
        // Check if a reset is needed
        CheckForResetRequest();
    }

    // Update grab-related components based on selection status
    private void ChangeActiveGrab()
    {
        // Skip if there is no selection data
        if (_selectData.Value is null)
            return;

        // Skip if the object is selected by the local client
        if (_selectData.Value._ownerId == ClientManager.Connection.ClientId)
            return;

        // Make the Rigidbody kinematic if selected
        _rigidbody.isKinematic = _selectData.Value._isSelected;
        
        // Disable the XRGrabInteractable and XRGeneralGrabTransformer component if selected
        _xrGrabInteractable.enabled = !_selectData.Value._isSelected;
        _xrGrabTransformer.enabled = !_selectData.Value._isSelected;
    }

    // Server-side method to check and reset selection if needed
    [ServerRpc(RequireOwnership = false)]
    private void CheckForResetRequest()
    {
        CheckForReset();
    }

    // Client-side method to check and reset selection if needed
    [ObserversRpc]
    private void CheckForReset()
    {
        // Skip if the object is not selected
        if (!_selectData.Value._isSelected)
            return;

        // Skip if the owner ID is not valid
        if (!_selectData.Value.IsValid)
            return;

        // If the client owning the object is no longer valid, reset selection
        if (ClientManager.Clients.TryGetValue(_selectData.Value._ownerId, out var connection))
            return;

        // Reset the selection status
        SetIsSelected(-1, false);
    }
}

[Serializable]
public class SyncSelectData
{
    public int _ownerId;
    public bool _isSelected;

    // Property to check if the owner ID is valid
    public bool IsValid => _ownerId >= 0;

    // Default constructor
    public SyncSelectData()
    {
        _ownerId = -1;
        _isSelected = false;
    }

    // Constructor with parameters
    public SyncSelectData(int ownerId, bool isSelected)
    {
        _ownerId = ownerId;
        _isSelected = isSelected;
    }
}