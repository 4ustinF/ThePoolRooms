using System.Collections;
using UnityEngine;

public class LeafEvent : MonoBehaviour, IGameEvent
{
    [Header("References")]
    [SerializeField] private ParticleSystem _particleSystem = null;
    [SerializeField] private AudioSource _audioSource = null;

    [Header("Varibles")]
    [SerializeField] private float _maxVolume = 0.7f;
    [SerializeField] private float _fadeInTime = 1.5f;
    [SerializeField] private float _fadeOutTime = 10.0f;

    public void StartGameEvent()
    {
        _particleSystem.Play();
        StartCoroutine(LeafVolumeFadeIn());
    }

    public void StopGameEvent()
    {
        _particleSystem.Stop();
        StartCoroutine(LeafVolumeFadeOut());
    }

    private IEnumerator LeafVolumeFadeIn()
    {
        _audioSource.volume = 0.0f;
        _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume += _maxVolume * (Time.deltaTime / _fadeInTime);
            yield return null;
        }

        _audioSource.volume = _maxVolume;
    }

    private IEnumerator LeafVolumeFadeOut()
    {
        _audioSource.volume = _maxVolume;
        while (_audioSource.volume > 0.0f)
        {
            _audioSource.volume -= _maxVolume * (Time.deltaTime / _fadeOutTime);
            yield return null;
        }

        _audioSource.volume = 0.0f;
        _audioSource.Stop();
    }

}
