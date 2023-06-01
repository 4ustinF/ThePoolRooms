﻿using System.Collections;
using Liminal.SDK.Core;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WaterFallManager _waterFallManager = null;
    [SerializeField] private SubtitleManager _subtitleManager = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private Animator _ballAnimator = null;

    [Header("DialougeAudioClips")]
    [SerializeField] private AudioClip _dialougeClip1 = null;
    [SerializeField] private AudioClip _dialougeClip2 = null;
    [SerializeField] private AudioClip _dialougeClip3 = null;
    [SerializeField] private AudioClip _dialougeClip4 = null;
    [SerializeField] private AudioClip _dialougeClip5 = null;
    [SerializeField] private AudioClip _dialougeClip6 = null;
    [SerializeField] private AudioClip _dialougeClip7 = null;
    [SerializeField] private AudioClip _dialougeClip8 = null;
    [SerializeField] private AudioClip _dialougeClip9 = null;
    [SerializeField] private AudioClip _dialougeClip10 = null;
    [SerializeField] private AudioClip _dialougeClip11 = null;
    [SerializeField] private AudioClip _dialougeClip12 = null;
    [SerializeField] private AudioClip _dialougeClip13 = null;

    private void Awake()
    {
        ExperienceApp.Initializing += Initialize;
    }

    public void Initialize()
    {
        ///make this 5 again before pushing
        StartCoroutine(WaitAndInvokeFunc(0.0f, PlayDialog1));
    }

    private IEnumerator WaitAndInvokeFunc(float waitTime, UnityAction func)
    {
        yield return new WaitForSeconds(waitTime);
        func.Invoke();
    }

    private IEnumerator WaitAndInvokeAudio(float waitTime, AudioClip clip)
    {
        yield return new WaitForSeconds(waitTime);
        _audioSource.PlayOneShot(clip);
    }

    #region ---AudioEvents---

    private void PlayDialog1()
    {
        Debug.Log("PlayDialog1");
        _audioSource.PlayOneShot(_dialougeClip1);

        float waitTime = _dialougeClip1.length;
        StartCoroutine(PlaySubtitle(1, 1, 2.0f, 0.5f));

        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallFallingAnim));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog2));
    }

    private void PlayDialog2()
    {
        _audioSource.PlayOneShot(_dialougeClip2);

        float time1 = 5.25f;
        float time2 = time1 + 0.75f;
        StartCoroutine(PlaySubtitle(2, 1, time1, 0.5f));
        StartCoroutine(PlaySubtitle(2, 2, 5.0f, time2));

        StartCoroutine(WaitAndInvokeFunc(_dialougeClip2.length + 2.0f, PlayDialog3));
    }

    private void PlayDialog3()
    {
        _audioSource.PlayOneShot(_dialougeClip3);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip3.length + 0.25f, _dialougeClip4));

        StartCoroutine(PlaySubtitle(3, 1, 5.5f, 0.5f));
        StartCoroutine(PlaySubtitle(3, 2, _dialougeClip4.length - 2.0f, _dialougeClip3.length + 0.75f));

        float waitTime = _dialougeClip3.length + _dialougeClip4.length;
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, TurnOnWaterFall));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog4));
    }

    private void PlayDialog4()
    {
        _audioSource.PlayOneShot(_dialougeClip5);

        _subtitleManager?.ReadSpecificLine(4, 1, _dialougeClip5.length - 2.0f);

        StartCoroutine(WaitAndInvokeFunc(_dialougeClip4.length + 0.25f, PlayDialog5));
    }

    private void PlayDialog5()
    {
        _audioSource.PlayOneShot(_dialougeClip6);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip6.length + 0.25f, _dialougeClip7));
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip7.length + _dialougeClip6.length + 0.5f, _dialougeClip8));

        _subtitleManager?.ReadSpecificLine(5, 1, _dialougeClip6.length - 2f);
        StartCoroutine(PlaySubtitle(5, 2, _dialougeClip7.length - 2f, _dialougeClip6.length + 0.25f));
        StartCoroutine(PlaySubtitle(5, 3, 6.0f, _dialougeClip7.length + _dialougeClip6.length + 0.5f));
        StartCoroutine(PlaySubtitle(5, 4, 7.0f, _dialougeClip7.length + _dialougeClip6.length + 7.5f));

        float waitTime = _dialougeClip6.length + _dialougeClip7.length + _dialougeClip8.length + 0.5f;
        StartCoroutine(WaitAndInvokeFunc(waitTime + 4.5f, _waterFallManager.StopWaterFall));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 5.0f, PlayDialog6));

    }

    private void PlayDialog6()
    {
        _audioSource.PlayOneShot(_dialougeClip9);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip9.length + 0.25f, _dialougeClip10));

        StartCoroutine(PlaySubtitle(6, 1, _dialougeClip9.length - 2.0f, 0.5f));
        StartCoroutine(PlaySubtitle(6, 2, _dialougeClip10.length - 2.0f, _dialougeClip9.length + 0.25f));

        float waitTime = _dialougeClip9.length + _dialougeClip10.length + 0.25f;
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayBallExitTunnel));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog7));
    }

    private void PlayDialog7()
    {
        _audioSource.PlayOneShot(_dialougeClip11);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip11.length + 0.25f, _dialougeClip12));
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip11.length + _dialougeClip12.length + 0.5f, _dialougeClip13));

        StartCoroutine(PlaySubtitle(7, 1, _dialougeClip11.length - 2f, 0.5f));
        StartCoroutine(PlaySubtitle(7, 2, _dialougeClip12.length - 2f, _dialougeClip11.length + 0.25f));
        StartCoroutine(PlaySubtitle(7, 3, 6.0f, _dialougeClip11.length + _dialougeClip12.length + 0.5f));
        StartCoroutine(PlaySubtitle(7, 4, 4.0f, _dialougeClip11.length + _dialougeClip12.length + 8.5f));

        //StartCoroutine(WaitAndInvokeFunc(_dialougeClip7.length, _waterFallManager.StopWaterFall));
    }

    private IEnumerator PlaySubtitle(int lineNum, int linePartNum, float lineSpeed, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _subtitleManager?.ReadSpecificLine(lineNum, linePartNum, lineSpeed);
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

        _ballAnimator?.Play("Falling");
        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallIdle1));
    }

    private void PlayBallIdle1()
    {
        _ballAnimator?.CrossFadeInFixedTime("Idle", 0.1f);
    }

    private void TurnOnWaterFall()
    {
        _waterFallManager?.StartWaterFall();
        StartCoroutine(WaitAndInvokeFunc(0.5f, PlayBallEnterTunnel)); // Time for waterfall to finish full throttle
    }

    private void PlayBallEnterTunnel()
    {
        _ballAnimator?.CrossFadeInFixedTime("EnterTunnel", 0.5f);
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

        _ballAnimator?.Play("ExitTunnel");
        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallIdle2));
    }

    private void PlayBallIdle2()
    {
        _ballAnimator?.CrossFadeInFixedTime("Idle2", 0.5f);
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
