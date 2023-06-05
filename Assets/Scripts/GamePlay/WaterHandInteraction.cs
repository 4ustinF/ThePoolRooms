using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterHandInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<GameObject> _waterSplashes = new List<GameObject>();
    [SerializeField] private List<GameObject> _waterRipples = new List<GameObject>();
    [SerializeField] private AudioSource _audioSource = null;

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.tag, "Player", System.StringComparison.OrdinalIgnoreCase) == 0)
        {
            _audioSource.Play();
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);

            SplashWater(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
        }
    }

    public void SplashWater(Vector3 pos)
    {
        foreach (GameObject p in _waterSplashes)
        {
            if (p.activeInHierarchy == false)
            {
                p.transform.position = pos;
                p.SetActive(true);
                break;
            }
        }

        foreach (GameObject ripple in _waterRipples)
        {
            if (ripple.activeInHierarchy == false)
            {
                ripple.transform.position = pos;
                ripple.SetActive(true);
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SplashWater(eventData.pointerCurrentRaycast.worldPosition);
    }
}
