using UnityEngine;
using UnityEngine.EventSystems;

public class BallManager : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObject _ripples = null;
    [SerializeField] private ParticleSystem _waterSplashPS = null;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _impactAudioClip = null;
    [SerializeField] private AudioClip _ballBounceAudioClip = null;

    private void PlayWaterSplash()
    {
        // TODO: Optimize this
        _waterSplashPS.transform.rotation = Quaternion.FromToRotation(_waterSplashPS.transform.eulerAngles, Vector3.up);
        _waterSplashPS.gameObject.SetActive(true);

        _audioSource.pitch = Random.Range(0.85f, 1.15f);
        _audioSource.volume = 0.2f;
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
        _audioSource.volume = 0.1f;
        _audioSource.clip = _ballBounceAudioClip;
        _audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.EndExperience();
    }
}
