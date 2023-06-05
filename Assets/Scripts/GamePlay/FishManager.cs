using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fishParticles = null;

    public void StartFishParticles()
    {
        _fishParticles.Play();
    }

    public void StopFishParticles()
    {
        _fishParticles.Stop();
    }
}
