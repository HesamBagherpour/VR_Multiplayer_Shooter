using UnityEngine;

public class OculusWrapper : MonoBehaviour
{
    public OVRCameraRig CameraRig;

    public bool Initialized { get; private set; }
    
    public Camera HeadCamera => CameraRig.gameObject.GetComponentInChildren<Camera>();
    public Camera HeadRightCamera => CameraRig.usePerEyeCameras ? CameraRig.rightEyeCamera : CameraRig.gameObject.GetComponentInChildren<Camera>();
    public Camera HeadLeftCamera => CameraRig.usePerEyeCameras ? CameraRig.leftEyeCamera : CameraRig.gameObject.GetComponentInChildren<Camera>();
    public Transform HeadTransform => CameraRig.centerEyeAnchor != null ? CameraRig.centerEyeAnchor.transform : null;
    public Transform RightHandTransform => CameraRig.rightHandAnchor != null ? CameraRig.rightHandAnchor.transform : null;
    public Transform LeftHandTransform => CameraRig.leftHandAnchor != null ? CameraRig.leftHandAnchor.transform : null;

    public Vector3 HeadPosition => HeadTransform != null ? HeadTransform.position : Vector3.zero;
    public Quaternion HeadRotation => HeadTransform != null ? HeadTransform.rotation : Quaternion.identity;

    public Vector3 RightHandPosition => RightHandTransform != null ? RightHandTransform.position : Vector3.zero;
    public Quaternion RightHandRotation => RightHandTransform != null ? RightHandTransform.rotation : Quaternion.identity;

    public Vector3 LeftHandPosition => LeftHandTransform != null ? LeftHandTransform.position : Vector3.zero;
    public Quaternion LeftHandRotation => LeftHandTransform != null ? LeftHandTransform.rotation : Quaternion.identity;

    public bool Init()
    {
        
        if (CameraRig == null)
        {
            CameraRig = FindObjectOfType<OVRCameraRig>().GetComponent<OVRCameraRig>();
            if (CameraRig == null)
            {
                Debug.LogError("No OVRCameraRig assigned. Please assign it in the inspector.");
                return false;
            }

        }

        if (CameraRig.rightHandAnchor == null || CameraRig.leftHandAnchor == null)
        {
            Debug.LogError("OVRCameraRig is missing hand anchors.");
            return false;
        }

        Initialized = true;
        return true;
    }
}