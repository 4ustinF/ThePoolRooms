using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Ambience")]
    [SerializeField] AudioClip _backgroundMusic = null;
    [SerializeField] AudioClip _waterAmbience = null;
    [SerializeField] AudioClip _waterFallAmbience = null;

    [Header("SFX")]
    [SerializeField] AudioClip _waterSplashSFX= null;

}
