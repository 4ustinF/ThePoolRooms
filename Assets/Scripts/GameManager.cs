using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum CurrentEvent
    {
        None = -1,
        Event1,
        Event2,
        Event3,
        Event4,
        Event5,
        Event6,
        Event7,
        Count
    }

    [Header("References")]
    [SerializeField] private WaterFallManager _waterFallManager = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private Animator _ballAnimator = null;

    //[Header("Ambience")]
    //[SerializeField] private AudioClip _backgroundMusic = null;
    //[SerializeField] private AudioClip _waterAmbience = null;

    //[Header("SFX")]
    //[SerializeField] private AudioClip _waterSplashSFX = null;

    [Header("DialougeAudioClips")]
    [SerializeField] private AudioClip _dialougeClip1 = null;
    [SerializeField] private AudioClip _dialougeClip2 = null;
    [SerializeField] private AudioClip _dialougeClip3 = null;
    [SerializeField] private AudioClip _dialougeClip4 = null;
    [SerializeField] private AudioClip _dialougeClip5 = null;
    [SerializeField] private AudioClip _dialougeClip6 = null;
    [SerializeField] private AudioClip _dialougeClip7 = null;

    [SerializeField] private CurrentEvent _currentEvent = CurrentEvent.None;

    private void Start()
    {
        MoveToNextEvent();
    }

    private void MoveToNextEvent()
    {
        int newEvent = (int)_currentEvent + 1;
        int maxEvent = (int)CurrentEvent.Count;
        _currentEvent = (CurrentEvent)Mathf.Min(newEvent, maxEvent);
        PlayAudio();
    }

    private void PlayAudio()
    {
        switch (_currentEvent)
        {
            case CurrentEvent.Event1:
                PlayEvent(_dialougeClip1, _dialougeClip1.length + 1.0f);
                break;
            case CurrentEvent.Event2:
                PlayEvent(_dialougeClip2, _dialougeClip2.length + 1.0f);
                break;
            case CurrentEvent.Event3:
                PlayEvent(_dialougeClip3, _dialougeClip3.length + 1.0f);
                break;
            case CurrentEvent.Event4:
                PlayEvent(_dialougeClip4, _dialougeClip4.length + 1.0f);
                break;
            case CurrentEvent.Event5:
                PlayEvent(_dialougeClip5, _dialougeClip5.length + 1.0f);
                break;
            case CurrentEvent.Event6:
                PlayEvent(_dialougeClip6, _dialougeClip6.length + 1.0f);
                break;
            case CurrentEvent.Event7:
                PlayEvent(_dialougeClip7, _dialougeClip7.length + 1.0f);
                break;
        }
    }

    private void PlayEvent(AudioClip clip, float eventWaitTime)
    {
        _audioSource.PlayOneShot(clip);
        StartCoroutine(PlayEventAnimationRoutine(eventWaitTime));
    }

    private IEnumerator PlayEventAnimationRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayEventAnimation();
    }

    private void PlayEventAnimation()
    {
        switch (_currentEvent)
        {
            case CurrentEvent.Event1: // Ball starts to descend stairs located in the middle of the room
                _ballAnimator?.Play("Falling");
                break;
            case CurrentEvent.Event2: // Ball reaches the water and starts to float without moving much
                _ballAnimator?.Play("Idle");
                break;
            case CurrentEvent.Event3: // A waterfall located at the left of the player turns on and attracts the attention.
                _waterFallManager?.StartWaterFall();
                break;
            case CurrentEvent.Event4: // Due to the waterfall, ball starts to move in the opposite direction and head towards a tunnel
                _ballAnimator?.Play("EnterTunnel");
                break;
            case CurrentEvent.Event5: // Ball reaches the tunnel and “swirls” into the darkness. Only music and sound effects will occur during the following 10 seconds to generate suspense
                break;
            case CurrentEvent.Event6: // Ball shows up at the other end of the tunnel and starts to move towards the player
                _ballAnimator?.Play("ExitTunnel");
                break;
            case CurrentEvent.Event7: // Ball reaches the player and they are able to pick it up. Either picking the ball up or 10 seconds pass after it reaches the player will end the experience
                _ballAnimator?.Play("Idle2");
                break;
        }

        Debug.Log($"Played event: {_currentEvent}");
        MoveToNextEvent();
    }


}

//Plot: Path through maturity and self understanding

// Event1
// Audio: Life begins with a stumble. 
// Action: Ball starts to descend stairs located in the middle of the room

// Event2
// Audio: Just as this ball awkwardly descends the stairs, everyone begins their journey clumsily, relying on momentum and instinct over purposeful smooth decision making. 
// Action: Ball reaches the water and starts to float without moving much

// Event3
// Audio: Everything is a first, but your destination is clear and although there may be bumps along your road, you know where you are going and there is a comfort in that.
// Action: A waterfall located at the left of the player turns on and attracts the attention.

// Event4
// Audio: But once you’ve adjusted to the basics of life, adolescence begins and everything you thought you knew changes once again. 
// Action: Due to the waterfall, ball starts to move in the opposite direction and head towards a tunnel

// Event5
// Audio: Your life can begin to drift in a direction you never expected and your destination can become more and more unclear. Who are you? Where are you going? The future is obscured and only by going forward can we find the answer to these questions
// Action: Ball reaches the tunnel and “swirls” into the darkness. Only music and sound effects will occur during the following 10 seconds to generate suspense

// Event6
// Audio: Ambiguity cannot last forever. Eventually everyone comes to an understanding of who they are and what they want out of life. The darkness is gone and a clear road lies ahead. 
// Action: Ball shows up at the other end of the tunnel and starts to move towards the player

// Event7
// Audio: There may be troubles ahead. Anything could happen. Will you be able to make your way through them? Once you know who you are, the tools to deal with whatever may come are right there for you. All you need … is to pick them up
// Action: Ball reaches the player and they are able to pick it up. Either picking the ball up or 10 seconds pass after it reaches the player will end the experience
