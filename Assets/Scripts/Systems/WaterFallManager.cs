using System.Collections;
using UnityEngine;

public class WaterFallManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
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

        _waterFallPS.Stop();
        _dustFallPS.Stop();
        _wavesPS.Stop();
        _sprinklesPS.Stop();
        _surfaceDust1PS.Stop();
        _surfaceDust2PS.Stop();
    }
}
