using Liminal.SDK.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHandInteraction : MonoBehaviour
{
    [SerializeField] private List<GameObject> _waterSplashes = new List<GameObject>();
    [SerializeField] private AudioSource _audioSource = null;

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.tag, "Player", System.StringComparison.OrdinalIgnoreCase) == 0)
        {
            _audioSource.Play();
            foreach (GameObject p in _waterSplashes)
            {
                if (p.activeInHierarchy == false)
                {
                    OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                    OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);

                    p.transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
                    p.SetActive(true);
                    break;
                }
            }
        }
    }

}
