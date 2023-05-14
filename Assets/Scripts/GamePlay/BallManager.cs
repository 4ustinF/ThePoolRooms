using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _waterSplashPS = null;

    private void PlayWaterSplash()
    {
        // TODO: Optimize this
        _waterSplashPS.transform.rotation = Quaternion.FromToRotation(_waterSplashPS.transform.eulerAngles, Vector3.up);
        _waterSplashPS.gameObject.SetActive(true);
        _waterSplashPS.Play();
    }


}
