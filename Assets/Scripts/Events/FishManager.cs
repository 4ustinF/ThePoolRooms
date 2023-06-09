﻿using System.Collections;
using UnityEngine;

public class FishManager : MonoBehaviour, IGameEvent
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private ParticleSystem _fishParticles = null;

    [Header("Varibles")]
    [SerializeField] private float _maxVolume = 0.3f;
    [SerializeField] private float _fadeInTime = 1.5f;
    [SerializeField] private float _fadeOutTime = 12.0f;

    public void StartGameEvent()
    {
        _fishParticles.Play();
        StartCoroutine(FishVolumeFadeIn());
    }

    public void StopGameEvent()
    {
        _fishParticles.Stop();
        StartCoroutine(FishVolumeFadeOut());
    }

    private IEnumerator FishVolumeFadeIn()
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

    private IEnumerator FishVolumeFadeOut()
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
