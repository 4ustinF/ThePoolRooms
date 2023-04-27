using System.Collections;
using UnityEngine;

public class WaterFallManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private ParticleSystem _waterFallPS = null;
    [SerializeField] private ParticleSystem _dustFallPS = null;
    [SerializeField] private ParticleSystem _wavesPS = null;
    [SerializeField] private ParticleSystem _sprinklesPS = null;
    [SerializeField] private ParticleSystem _surfaceDust1PS = null;
    [SerializeField] private ParticleSystem _surfaceDust2PS = null;
    [SerializeField] private float _waitTime = 1.5f;

    public void StartWaterFall()
    {
        _waterFallPS.Play();
        _dustFallPS.Play();
        StartCoroutine(StartWaterDebris());

        // Start Audio
        _audioSource.volume = 1.0f;
        _audioSource.Play();
    }

    private IEnumerator StartWaterDebris()
    {
        yield return new WaitForSeconds(_waitTime);
        _wavesPS.Play();
        _sprinklesPS.Play();
        _surfaceDust1PS.Play();
        _surfaceDust2PS.Play();
    }

    public void StopWaterFall()
    {
        // Kill StartWaterDebris routine if its active
        
        // Stop particle spawning
        _waterFallPS.Stop();
        _dustFallPS.Stop();
        _wavesPS.Stop();
        _sprinklesPS.Stop();
        _surfaceDust1PS.Stop();
        _surfaceDust2PS.Stop();

        // Stop audio
        _audioSource.Stop();
        StartCoroutine(WaterFallVolumeFade());
    }

    private IEnumerator WaterFallVolumeFade()
    {
        float currentTime = 0.0f;
        float maxTime = 5.0f;

        while(currentTime < maxTime)
        {
            currentTime += Time.deltaTime; // TODO: Dont call Time.deltaTime
            _audioSource.volume = 1.0f - (currentTime / maxTime);
            yield return null;
        }

        _audioSource.Stop();
    }
}
