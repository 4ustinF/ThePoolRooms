using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterHandInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<GameObject> _waterSplashes = new List<GameObject>();
    [SerializeField] private List<GameObject> _waterRipples = new List<GameObject>();
    [SerializeField] private AudioSource _audioSource = null;

    private float _frequency = 0.5f;
    private float _amplitude = 0.5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        SplashWater(eventData.pointerCurrentRaycast.worldPosition);

        //Debug.Log(eventData.pointerId);
        //if(eventData.pointerId == 0)
        //{
        //    OVRInput.SetControllerVibration(_frequency, _amplitude, OVRInput.Controller.LTouch);
        //}
        //else if(eventData.pointerId == 1) 
        //{
        //    OVRInput.SetControllerVibration(_frequency, _amplitude, OVRInput.Controller.RTouch);
        //}
    }

    private void SplashWater(Vector3 pos)
    {
        _audioSource.Play();

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
}

// OVRInput.SetControllerVibration(_frequency, _amplitude, OVRInput.Controller.RTouch);
// OVRInput.SetControllerVibration(_frequency, _amplitude, OVRInput.Controller.LTouch);