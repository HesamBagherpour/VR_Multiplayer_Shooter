using UnityEngine;

public class RemoteVRPlayer : VRPlayer
{
    private float _lerpSpeed;

    protected override void Init()
    {
        base.Init();

        _lerpSpeed = 25f;
    }

    private void Update()
    {
        // Smoothly update remote player's transform
        LerpToNextTransform();
    }

    public override void SetTransform(IKTransforms nextTransform)
    {
        // Update the next transform to interpolate towards
        _nextTransform = nextTransform;
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
}
