using UnityEngine;

public class WaterfallParticle : MonoBehaviour
{
    public ParticleSystem particleSystem = null;
    public float maxEmissionRate = 0;

    public ParticleSystem.EmissionModule GetAndSetEmission(float amount)
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = amount;
        return emission;
    }
}
