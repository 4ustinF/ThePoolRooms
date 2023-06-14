using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour, IGameEvent
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private ParticleSystem _fishParticles = null;
    [SerializeField] private Vector2 _waitTime = new Vector2(1.0f, 1.5f);
    [SerializeField] private List<GameObject> _ripples = new List<GameObject>();

    [Header("Varibles")]
    [SerializeField] private float _maxVolume = 0.3f;
    [SerializeField] private float _fadeInTime = 1.5f;
    [SerializeField] private float _fadeOutTime = 12.0f;

    private IEnumerator _rippleRoutine = null;

    public void StartGameEvent()
    {
        _fishParticles.Simulate(3.9f, true);
        _fishParticles.Play();

        StartCoroutine(FishVolumeFadeIn());

        _rippleRoutine = RippleRoutine(Random.Range(_waitTime.x, _waitTime.y));
        StartCoroutine(_rippleRoutine);
    }

    public void StopGameEvent()
    {
        _fishParticles.Stop();
        StartCoroutine(FishVolumeFadeOut());

        StopCoroutine(_rippleRoutine);
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

    private IEnumerator RippleRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        while (true)
        {
            GameObject drip = _ripples[Random.Range(0, _ripples.Count)];
            if (drip.activeInHierarchy == false)
            {
                drip.transform.position = new Vector3(Random.Range(-24.5f, 14.5f), 1.75f, Random.Range(11.0f, 13.0f));
                drip.SetActive(true);
                break;
            }
            yield return null;
        }

        _rippleRoutine = RippleRoutine(Random.Range(_waitTime.x, _waitTime.y));
        StartCoroutine(_rippleRoutine);
    }

}
