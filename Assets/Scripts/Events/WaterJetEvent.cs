using System.Collections;
using UnityEngine;

public class WaterJetEvent : MonoBehaviour, IGameEvent
{
    [SerializeField] private ParticleSystem _particleSystem = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private float _maxVolume = 0.15f;

    public void StartGameEvent()
    {
        _particleSystem.Play();
        _audioSource.Play();
        VolumeFadeIn(_maxVolume, 1.0f);
    }

    public void StopGameEvent()
    {
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
}
