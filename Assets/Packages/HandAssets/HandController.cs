using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private IVRInputDevice _primaryInput = null;
    [SerializeField] private Material _handMat = null;
    [SerializeField] private Transform _rightHandTransformTarget = null;
    [SerializeField] private float _smoothingFactor = 0.85f; // Adjust this value to control the amount of smoothing

    private Vector3 _currentHandPosition = Vector3.zero;
    private Quaternion _currentHandRotation = Quaternion.identity;
    private Vector3 _smoothedHandPosition = Vector3.zero;
    private Quaternion _smoothedHandRotation = Quaternion.identity;

    private bool _isInitialized = false;

    private void Awake()
    {
        ExperienceApp.Initializing += Initialize;
    }

    public void Initialize()
    {
        _primaryInput = VRDevice.Device.PrimaryInputDevice;
        _isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInitialized == false)
        {
            return;
        }

        if (_primaryInput.GetButtonDown(VRButton.Primary)) // Liminal SDK
        {
            Debug.Log("Button Pressed!");
            _handMat.color = Color.red;
        }
        else if (_primaryInput.GetButtonUp(VRButton.Primary)) // Liminal SDK
        {
            Debug.Log("Button Pressed!");
            _handMat.color = Color.white;
        }

        UpdateRightHandPos();
    }

    private void UpdateRightHandPos()
    {
        // Update the current hand position and rotation
        _currentHandPosition = _rightHandTransformTarget.position;
        _currentHandRotation = _rightHandTransformTarget.rotation;

        // Apply low-pass filter to smooth hand movement
        _smoothedHandPosition = Vector3.Lerp(_smoothedHandPosition, _currentHandPosition, _smoothingFactor);
        _smoothedHandRotation = Quaternion.Slerp(_smoothedHandRotation, _currentHandRotation, _smoothingFactor);

        // Use the smoothed position and rotation for rendering or other purposes
        transform.position = _smoothedHandPosition;
        transform.rotation = _smoothedHandRotation;
    }

}

// _handTransformTarget Rotation = 0.0f, 7.56f, 270.0f
// https://liminalvr.notion.site/Controllers-9db31ad0357e4565922511cef7267095#10dc29d21a3a4703af7d2136e8cd172c
// OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
// OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);