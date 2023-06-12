using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallManager : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private SphereCollider _collider = null;
    [SerializeField] private GameObject _ripples = null;
    [SerializeField] private ParticleSystem _waterSplashPS = null;
    [SerializeField] private ParticleSystem _shimmerPS = null;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _impactAudioClip = null;
    [SerializeField] private AudioClip _ballBounceAudioClip = null;
    [SerializeField] private AudioClip _shimmerAudioClip = null;

    [Header("Volumes")]
    [SerializeField] private float _waterSplashVolume = 0.2f;
    [SerializeField] private float _bounceVolume = 0.1f;
    private float _shimmerVolume = 0.15f;

    private void PlayWaterSplash()
    {
        // TODO: Optimize this
        _waterSplashPS.transform.rotation = Quaternion.FromToRotation(_waterSplashPS.transform.eulerAngles, Vector3.up);
        _waterSplashPS.gameObject.SetActive(true);

        _audioSource.pitch = Random.Range(0.85f, 1.15f);
        _audioSource.volume = _waterSplashVolume;
        _audioSource.clip = _impactAudioClip;
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
        _audioSource.volume = _bounceVolume;
        _audioSource.clip = _ballBounceAudioClip;
        _audioSource.Play();
    }

    private void PlayShimmer()
    {
        _collider.enabled = true;
        _shimmerPS.gameObject.SetActive(true);

        _audioSource.clip = _shimmerAudioClip;
        _audioSource.loop = true;
        _audioSource.pitch = 1.0f;
        _audioSource.reverbZoneMix = 0.0f;
        StartCoroutine(VolumeFadeIn(_shimmerVolume, 2.0f));

        _audioSource.Play();
    }

    private IEnumerator VolumeFadeIn(float volume, float waitTime)
    {
        _audioSource.volume = 0.0f;
        _audioSource.Play();

        while (_audioSource.volume < volume)
        {
            _audioSource.volume += volume * Time.deltaTime / waitTime;
            yield return null;
        }

        _audioSource.volume = volume;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.EndExperience();
    }
}
