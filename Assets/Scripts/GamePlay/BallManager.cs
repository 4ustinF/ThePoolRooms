using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject _ripples = null;
    [SerializeField] private ParticleSystem _waterSplashPS = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _splashAudioClip = null;
    [SerializeField] private AudioClip _ballBounceAudioClip = null;

    private void PlayWaterSplash()
    {
        // TODO: Optimize this
        _waterSplashPS.transform.rotation = Quaternion.FromToRotation(_waterSplashPS.transform.eulerAngles, Vector3.up);
        _waterSplashPS.gameObject.SetActive(true);

        _audioSource.pitch = Random.Range(0.85f, 1.15f);
        _audioSource.volume = 0.15f;
        _audioSource.clip = _splashAudioClip;
        _audioSource.Play();

        _waterSplashPS.Play();
    }

    private void TurnOnRippleParticles()
    {
        _ripples.SetActive(true);
    }

    private void PlayBounce()
    {
        _audioSource.pitch = Random.Range(0.85f, 1.15f);
        _audioSource.volume = 0.07f;
        _audioSource.clip = _ballBounceAudioClip;
        _audioSource.Play();
    }


}
