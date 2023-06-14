using System.Collections;
using UnityEngine;

public class WaterFallManager : MonoBehaviour, IGameEvent
{
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private WaterfallParticle _waterFallPS = null;
    //[SerializeField] private WaterfallParticle _dustFallPS = null;
    [SerializeField] private WaterfallParticle _sprinklesPS = null;
    //[SerializeField] private WaterfallParticle _surfaceDust1PS = null;
    [SerializeField] private WaterfallParticle _surfaceDust2PS = null;
    [SerializeField] private float _maxVolume = 0.15f;
    [SerializeField] private float _fadeInTime = 3.0f;
    [SerializeField] private float _fadeOutTime = 1.4f;

    public void StartGameEvent()
    {
        StartCoroutine(FadeInWaterFalling());
        StartCoroutine(FadeInWaterDebris());

        // Start Audio
        StartCoroutine(WaterFallVolumeFadeIn());
    }

    public void StopGameEvent()
    {
        // Stop particle spawning
        StartCoroutine(FadeOutWaterFalling());

        // Stop audio
        StartCoroutine(WaterFallVolumeFadeOut());
    }

    private IEnumerator FadeInWaterFalling()
    {
        var _waterFallEmission = _waterFallPS.GetAndSetEmission(0.0f);
        //var _dustFallEmission = _dustFallPS.GetAndSetEmission(0.0f);

        _waterFallPS.particleSystem.Play();
        //_dustFallPS.particleSystem.Play();

        float timer = 0.0f;
        float maxTime = 4.0f;

        while (timer < maxTime)
        {
            timer += Time.deltaTime;
            float percent = timer / maxTime;

            _waterFallEmission.rateOverTime = Mathf.Lerp(0.0f, _waterFallPS.maxEmissionRate, percent);
            //_dustFallEmission.rateOverTime = Mathf.Lerp(0.0f, _dustFallPS.maxEmissionRate, percent);

            yield return null;
        }

        _waterFallEmission.rateOverTime = _waterFallPS.maxEmissionRate;
        //_dustFallEmission.rateOverTime = _dustFallPS.maxEmissionRate;
    }

    private IEnumerator FadeOutWaterFalling()
    {
        var _waterFallEmission = _waterFallPS.GetAndSetEmission(_waterFallPS.maxEmissionRate);
        //var _dustFallEmission = _dustFallPS.GetAndSetEmission(_dustFallPS.maxEmissionRate);
        var _sprinklesEmission = _sprinklesPS.GetAndSetEmission(_sprinklesPS.maxEmissionRate);
        //var _surfaceDustEmission = _surfaceDust1PS.GetAndSetEmission(_surfaceDust1PS.maxEmissionRate);
        var _surfaceDus2Emission = _surfaceDust2PS.GetAndSetEmission(_surfaceDust2PS.maxEmissionRate);

        float timer = 0.0f;
        float maxTime = 1.5f;

        while (timer < maxTime)
        {
            timer += Time.deltaTime;
            float percent = timer / maxTime;

            _waterFallEmission.rateOverTime = Mathf.Lerp(_waterFallPS.maxEmissionRate, 0.0f, percent);
            //_dustFallEmission.rateOverTime = Mathf.Lerp(_dustFallPS.maxEmissionRate, 0.0f, percent);
            _sprinklesEmission.rateOverTime = Mathf.Lerp(_sprinklesPS.maxEmissionRate, 0.0f, percent);
            //_surfaceDustEmission.rateOverTime = Mathf.Lerp(_surfaceDust1PS.maxEmissionRate, 0.0f, percent);
            _surfaceDus2Emission.rateOverTime = Mathf.Lerp(_surfaceDust2PS.maxEmissionRate, 0.0f, percent);

            yield return null;
        }

        _waterFallEmission.rateOverTime = 0.0f;
        //_dustFallEmission.rateOverTime = 0.0f;
        _sprinklesEmission.rateOverTime = 0.0f;
        //_surfaceDustEmission.rateOverTime = 0.0f;
        _surfaceDus2Emission.rateOverTime = 0.0f;

        _waterFallPS.particleSystem.Stop();
        //_dustFallPS.particleSystem.Stop();
        _sprinklesPS.particleSystem.Stop();
        //_surfaceDust1PS.particleSystem.Stop();
        _surfaceDust2PS.particleSystem.Stop();
    }

    private IEnumerator FadeInWaterDebris()
    {
        yield return new WaitForSeconds(0.5f);

        var _sprinklesEmission = _sprinklesPS.GetAndSetEmission(0.0f);
        //var _surfaceDustEmission = _surfaceDust1PS.GetAndSetEmission(0.0f);
        var _surfaceDus2Emission = _surfaceDust2PS.GetAndSetEmission(0.0f);

        _sprinklesPS.particleSystem.Play();
        //_surfaceDust1PS.particleSystem.Play();
        _surfaceDust2PS.particleSystem.Play();

        float timer = 0.0f;
        float maxTime = 3.0f;

        while (timer < maxTime)
        {
            timer += Time.deltaTime;
            float percent = timer / maxTime;

            _sprinklesEmission.rateOverTime = Mathf.Lerp(0.0f, _sprinklesPS.maxEmissionRate, percent);
            //_surfaceDustEmission.rateOverTime = Mathf.Lerp(0.0f, _surfaceDust1PS.maxEmissionRate, percent);
            _surfaceDus2Emission.rateOverTime = Mathf.Lerp(0.0f, _surfaceDust2PS.maxEmissionRate, percent);

            yield return null;
        }

        _sprinklesEmission.rateOverTime = _sprinklesPS.maxEmissionRate;
        //_surfaceDustEmission.rateOverTime = _surfaceDust1PS.maxEmissionRate;
        _surfaceDus2Emission.rateOverTime = _surfaceDust2PS.maxEmissionRate;
    }

    private IEnumerator WaterFallVolumeFadeIn()
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

    private IEnumerator WaterFallVolumeFadeOut()
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
