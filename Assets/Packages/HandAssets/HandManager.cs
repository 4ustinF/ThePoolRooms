using Liminal.SDK.Core;
using System.Collections.Generic;
using UnityEngine;

public enum HandPose
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

public class HandManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandController _rightHand = null;
    [SerializeField] private HandController _leftHand = null;

    private Dictionary<(bool, bool, bool), HandPose> _buttonCombinationToAnimation = new Dictionary<(bool, bool, bool), HandPose>(); // In order: Index, Middle, Thumb

    private void Awake()
    {
        ExperienceApp.Initializing += Initialize;
    }

    private void Initialize()
    {
        _buttonCombinationToAnimation[(false, false, false)] = HandPose.Idle;
        _buttonCombinationToAnimation[(false, true, false)] = HandPose.Point;
        _buttonCombinationToAnimation[(true, false, false)] = HandPose.Rounded;
        _buttonCombinationToAnimation[(false, false, true)] = HandPose.Four;
        _buttonCombinationToAnimation[(true, true, false)] = HandPose.ThumbsUp;
        _buttonCombinationToAnimation[(true, false, true)] = HandPose.Pinch;
        _buttonCombinationToAnimation[(false, true, true)] = HandPose.Sign;
        _buttonCombinationToAnimation[(true, true, true)] = HandPose.Closed;

        _rightHand.Initialize();
        _leftHand.Initialize();
    }

    public HandPose GetHandPose(bool isIndex, bool isMiddle, bool isThumb)
    {
        HandPose newHandPose = HandPose.Idle;
        _buttonCombinationToAnimation.TryGetValue((isIndex, isMiddle, isThumb), out newHandPose);

        return newHandPose;
    }
}
