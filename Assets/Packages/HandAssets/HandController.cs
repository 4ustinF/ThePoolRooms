using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections.Generic;
using UnityEngine;

enum HandPose
{
    Idle, // No Buttons
    Point, // Middle Finger 
    Rounded, // Index Finger 
    Four, // Thumb 
    ThumbsUp, // Middle Finger, Index Finger 
    Pinch, // Index finger, thumb
    Sign, // Middle finger, thumb ----------
    Closed, // Middle Finger, Index Finger, and thumb
}

public class HandController : MonoBehaviour
{
    private IVRInputDevice _inputDevice = null;
    [SerializeField] private bool _isRightHand = true;
    [SerializeField] private Animator _handAnimator = null;
    [SerializeField] private Transform _handTransformTarget = null; // The transform the hand smoothly lerps towards
    [SerializeField] private float _smoothingFactor = 0.85f; // Adjust this value to control the amount of smoothing

    private Vector3 _currentHandPosition = Vector3.zero;
    private Quaternion _currentHandRotation = Quaternion.identity;
    private Vector3 _smoothedHandPosition = Vector3.zero;
    private Quaternion _smoothedHandRotation = Quaternion.identity;

    private bool _isInitialized = false;

    // Hand Animations
    private HandPose _handpose = HandPose.Idle;
    private Dictionary<(bool, bool, bool), HandPose> _buttonCombinationToAnimation = new Dictionary<(bool, bool, bool), HandPose>(); // In order: Index, Middle, Thumb | TODO: We have 2 instances of this dictonary make it only 1

    private void Awake()
    {
        ExperienceApp.Initializing += Initialize;
    }

    public void Initialize()
    {
#if UNITY_EDITOR
        if (_isRightHand == false)
        {
            this.gameObject.SetActive(false);
            return;
        }
#endif
        _buttonCombinationToAnimation[(false, false, false)] = HandPose.Idle;
        _buttonCombinationToAnimation[(false, true, false)] = HandPose.Point;
        _buttonCombinationToAnimation[(true, false, false)] = HandPose.Rounded;
        _buttonCombinationToAnimation[(false, false, true)] = HandPose.Four;
        _buttonCombinationToAnimation[(true, true, false)] = HandPose.ThumbsUp;
        _buttonCombinationToAnimation[(true, false, true)] = HandPose.Pinch;
        _buttonCombinationToAnimation[(false, true, true)] = HandPose.Sign;
        _buttonCombinationToAnimation[(true, true, true)] = HandPose.Closed;

        _inputDevice = _isRightHand ? VRDevice.Device.PrimaryInputDevice : VRDevice.Device.SecondaryInputDevice;
        _isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInitialized == false)
        {
            return;
        }

        UpdateHandTransform();
        CalculateHandPose();
    }

    private void CalculateHandPose()
    {
        bool isIndex = _inputDevice.GetButton(VRButton.Primary);
        bool isMiddle = _inputDevice.GetButton(VRButton.Three);
        bool isThumb = (_inputDevice.GetButton(VRButton.Seconday) || _inputDevice.GetButton(VRButton.Four));

        HandPose newHandPose = HandPose.Idle;
        if (_buttonCombinationToAnimation.TryGetValue((isIndex, isMiddle, isThumb), out newHandPose))
        {
            if(_handpose == newHandPose)
            {
                return;
            }

            _handpose = newHandPose;
            _handAnimator.CrossFade(_handpose.ToString(), 0.1f);
        }
    }

    private void UpdateHandTransform()
    {
        // Update the current hand position and rotation
        _currentHandPosition = _handTransformTarget.position;
        _currentHandRotation = _handTransformTarget.rotation;

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
//_inputDevice.GetButtonDown(VRButton.Primary) // Left Index
//_inputDevice.GetButtonDown(VRButton.Three) // Middle finger
//_inputDevice.GetButtonDown(VRButton.Seconday) //Thumb joystick
//_inputDevice.GetButtonDown(VRButton.Four) // Thumb A button