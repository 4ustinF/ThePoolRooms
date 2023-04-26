using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager = null;

    public AudioManager GetAudioManager() { return _audioManager; }
}
