using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
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

    private void Start()
    {
        //StartCoroutine(WaitAndInvokeFunc(1.0f, PlayDialog1));
    }

    private IEnumerator WaitAndInvokeFunc(float waitTime, UnityAction func)
    {
        yield return new WaitForSeconds(waitTime);
        func.Invoke();
    }

    #region ---AudioEvents---

    private void PlayDialog1()
    {
        Debug.Log("PlayDialog1");
        _audioSource.PlayOneShot(_dialougeClip1);
        float waitTime = _dialougeClip1.length;
        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallFallingAnim));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog2));
    }

    private void PlayDialog2()
    {
        _audioSource.PlayOneShot(_dialougeClip2);
        StartCoroutine(WaitAndInvokeFunc(_dialougeClip2.length + 2.0f, PlayDialog3));
    }

    private void PlayDialog3()
    {
        _audioSource.PlayOneShot(_dialougeClip3);
        float waitTime = _dialougeClip3.length;
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, TurnOnWaterFall));
        StartCoroutine(WaitAndInvokeFunc(waitTime+ 0.25f, PlayDialog4));
    }

    private void PlayDialog4()
    {
        _audioSource.PlayOneShot(_dialougeClip4);
        StartCoroutine(WaitAndInvokeFunc(_dialougeClip4.length + 0.25f, PlayDialog5));
    }

    private void PlayDialog5()
    {
        _audioSource.PlayOneShot(_dialougeClip5);
        StartCoroutine(WaitAndInvokeFunc(_dialougeClip5.length + 5.0f, PlayDialog6));
    }

    private void PlayDialog6()
    {
        _audioSource.PlayOneShot(_dialougeClip6);
        float waitTime = _dialougeClip6.length;
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayBallExitTunnel));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog7));
    }

    private void PlayDialog7()
    {
        _audioSource.PlayOneShot(_dialougeClip7);
        StartCoroutine(WaitAndInvokeFunc(_dialougeClip7.length, _waterFallManager.StopWaterFall));
    }

    #endregion ---AudioEvents---

    #region ---AnimEvents---

    private void PlayBallFallingAnim()
    {
        AnimationClip[] clips = _ballAnimator?.runtimeAnimatorController.animationClips;
        float waitTime = 0.0f;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name == "Falling")
            {
                waitTime = clip.length;
                break;
            }
        }

        Debug.Log($"PlayBallFallingAnim: {waitTime}");

        _ballAnimator?.Play("Falling");
        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallIdle1));
    }

    private void PlayBallIdle1()
    {
        _ballAnimator?.Play("Idle");
        // TODO: Play ball splash particles
    }

    private void TurnOnWaterFall()
    {
        _waterFallManager?.StartWaterFall();
        StartCoroutine(WaitAndInvokeFunc(0.5f, PlayBallEnterTunnel)); // Time for waterfall to finish full throttle
    }

    private void PlayBallEnterTunnel()
    {
        _ballAnimator?.Play("EnterTunnel");
    }

    private void PlayBallExitTunnel()
    {
        AnimationClip[] clips = _ballAnimator?.runtimeAnimatorController.animationClips;
        float waitTime = 0.0f;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "ExitTunnel")
            {
                waitTime = clip.length;
                break;
            }
        }

        Debug.Log($"PlayBallExitTunnel: {waitTime}");
        _ballAnimator?.Play("ExitTunnel");
        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallIdle2));
    }

    private void PlayBallIdle2()
    {
        _ballAnimator?.Play("Idle2");
    }

    #endregion ---Events---


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
