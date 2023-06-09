using System.Collections;
using Liminal.Core.Fader;
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
    [SerializeField] private FishManager _fishManager = null;

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

    private bool _canEndExpereince = false;
    private bool _endingExpereince = false;

    private void Awake()
    {
        AudioListener.volume = 0.0f; // Prevent any sound from the scene. This is so we can fade audio in.
        ExperienceApp.Initializing += Initialize;
    }

    private void Initialize()
    {
        StartCoroutine(FadeInAudioListener(2.0f));
        StartCoroutine(WaitAndInvokeFunc(10.0f, PlayDialog1));
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

    IEnumerator FadeInAudioListener(float fadeTime)
    {
        var elapsedTime = 0.0f; // Instantiate a float with a value of 0 for use as a timer.
        var startingVolume = 0.0f; // This gets the current volume of the audio listener so that we can fade it to 1 over time.

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime; // Count up
            AudioListener.volume = Mathf.Lerp(startingVolume, 1.0f, elapsedTime / fadeTime); // This uses linear interpolation to change the volume of AudioListener over time.
            yield return new WaitForEndOfFrame(); // Tell the coroutine to wait for a frame to avoid completing this loop in a single frame.
        }

        AudioListener.volume = 1.0f;
    }

    public void EndExperience()
    {
        if(_canEndExpereince == false || _endingExpereince == true)
        {
            return;
        }
        _endingExpereince = true;

        StartCoroutine(FadeAndExit(2.0f));

        // This coroutine fades the camera and audio simultaneously over the same length of time.
        IEnumerator FadeAndExit(float fadeTime)
        {
            var elapsedTime = 0.0f; // Instantiate a float with a value of 0 for use as a timer.
            var startingVolume = AudioListener.volume; // This gets the current volume of the audio listener so that we can fade it to 0 over time.

            ScreenFader.Instance.FadeTo(Color.black, fadeTime); // Tell the system to fade the camera to black over X seconds where X is the value of fadeTime.

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime; // Count up
                AudioListener.volume = Mathf.Lerp(startingVolume, 0.0f, elapsedTime / fadeTime); // This uses linear interpolation to change the volume of AudioListener over time.
                yield return new WaitForEndOfFrame(); // Tell the coroutine to wait for a frame to avoid completing this loop in a single frame.
            }

            // When the while-loop has ended, the audiolistener volume should be 0 and the screen completely black. However, for safety's sake, we should manually set AudioListener volume to 0.
            AudioListener.volume = 0.0f;

            ExperienceApp.End(); // This tells the platform to exit the experience.
        }
    }

    #region ---AudioEvents---

    private void PlayDialog1()
    {
        Debug.Log("PlayDialog1");
        _audioSource.PlayOneShot(_dialougeClip1);

        float waitTime = _dialougeClip1.length + 2f;
        StartCoroutine(PlaySubtitle(1, 1, 2.0f, 0.5f));

        StartCoroutine(WaitAndInvokeFunc(waitTime, PlayBallFallingAnim));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog2));
    }

    private void PlayDialog2()
    {
        _audioSource.PlayOneShot(_dialougeClip2);

        float time1 = 6.25f;
        float time2 = time1 + 1f;
        StartCoroutine(PlaySubtitle(2, 1, time1, 0.5f));
        StartCoroutine(PlaySubtitle(2, 2, 5.0f, time2));

        StartCoroutine(WaitAndInvokeFunc(_dialougeClip2.length + 3.0f, PlayDialog3));
    }

    private void PlayDialog3()
    {
        _audioSource.PlayOneShot(_dialougeClip3);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip3.length + 1.25f, _dialougeClip4));

        StartCoroutine(PlaySubtitle(3, 1, _dialougeClip3.length - 2.0f, 0.5f));
        StartCoroutine(PlaySubtitle(3, 2, _dialougeClip4.length - 2.0f, _dialougeClip3.length + 1.75f));

        float waitTime = _dialougeClip3.length + _dialougeClip4.length + 7.0f;
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, TurnOnWaterFall));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.25f, PlayDialog4));
    }

    private void PlayDialog4()
    {
        _audioSource.PlayOneShot(_dialougeClip5);

        StartCoroutine(PlaySubtitle(4, 1, _dialougeClip5.length - 2.0f, 1f));

        StartCoroutine(WaitAndInvokeFunc(_dialougeClip4.length + 6f, PlayDialog5));
    }

    private void PlayDialog5()
    {
        _audioSource.PlayOneShot(_dialougeClip6);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip6.length + 0.25f, _dialougeClip7));
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip7.length + _dialougeClip6.length + 0.5f, _dialougeClip8));

        _subtitleManager?.ReadSpecificLine(5, 1, _dialougeClip6.length - 2f);
        StartCoroutine(PlaySubtitle(5, 2, _dialougeClip7.length - 2f, _dialougeClip6.length + 0.25f));
        StartCoroutine(PlaySubtitle(5, 3, 6.0f, _dialougeClip7.length + _dialougeClip6.length + 0.5f));
        StartCoroutine(PlaySubtitle(5, 4, 6.0f, _dialougeClip7.length + _dialougeClip6.length + 8f));

        StartCoroutine(WaitAndInvokeFunc(_dialougeClip7.length + _dialougeClip6.length + 13.5f, _fishManager.StartGameEvent));

        float waitTime = _dialougeClip6.length + _dialougeClip7.length + _dialougeClip8.length + 22.0f;
        StartCoroutine(WaitAndInvokeFunc(_dialougeClip7.length + _dialougeClip6.length + 8.5f, _waterFallManager.StopGameEvent));
        StartCoroutine(WaitAndInvokeFunc(waitTime, _fishManager.StopGameEvent));
        StartCoroutine(WaitAndInvokeFunc(waitTime + 0.5f, PlayDialog6));
    }

    private void PlayDialog6()
    {
        _audioSource.PlayOneShot(_dialougeClip9);
        StartCoroutine(WaitAndInvokeAudio(_dialougeClip9.length + 0.25f, _dialougeClip10));

        StartCoroutine(PlaySubtitle(6, 1, _dialougeClip9.length - 2.0f, 0.5f));
        StartCoroutine(PlaySubtitle(6, 2, _dialougeClip10.length - 2.0f, _dialougeClip9.length + 1.25f));

        float waitTime = _dialougeClip9.length + _dialougeClip10.length + 6.25f;
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
        StartCoroutine(PlaySubtitle(7, 3, 6.0f, _dialougeClip11.length + _dialougeClip12.length + 1f));
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
        _waterFallManager.StartGameEvent();
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
        _canEndExpereince = true;
    }

    #endregion ---Events---


}

