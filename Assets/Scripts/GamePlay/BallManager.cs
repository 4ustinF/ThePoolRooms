using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject _ripples = null;
    [SerializeField] private ParticleSystem _waterSplashPS = null;
    [SerializeField] private AudioSource _splashAudioSource = null;

    private void PlayWaterSplash()
    {
        // TODO: Optimize this
        _waterSplashPS.transform.rotation = Quaternion.FromToRotation(_waterSplashPS.transform.eulerAngles, Vector3.up);
        _waterSplashPS.gameObject.SetActive(true);

        _splashAudioSource.pitch = Random.Range(0.85f, 1.15f);
        _splashAudioSource.Play();

        _waterSplashPS.Play();
    }

    private void TurnOnRippleParticles()
    {
        _ripples.SetActive(true);
    }


}
