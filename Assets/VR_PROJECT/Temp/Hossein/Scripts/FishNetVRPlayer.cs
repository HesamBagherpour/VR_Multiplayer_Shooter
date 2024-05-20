using FishNet.Object;

public class FishNetVRPlayer : NetworkBehaviour
{
    private VRPlayer _vrPlayer;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
            _vrPlayer = gameObject.AddComponent<LocalVRPlayer>();
        else
            _vrPlayer = gameObject.AddComponent<RemoteVRPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Skip if the client is not initialized
        if (!IsClientInitialized)
            return;

        if (IsOwner) // Local player
        {
            var transform = _vrPlayer.NextTransform;
            SendNextTransforms(transform); // Send the local player's transform to the server
        }
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
        _vrPlayer.SetTransform(playerTransform);
    }
}