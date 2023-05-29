using Liminal.SDK.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHandInteraction : MonoBehaviour
{
    [SerializeField] private List<GameObject> _waterSplashes = new List<GameObject>();
    [SerializeField] private Transform _handTransform = null;


    private void OnTriggerEnter(Collider other)
    {
        //if (string.Compare(other.tag, "Player", System.StringComparison.OrdinalIgnoreCase) == 0)
        //{
        //    foreach (GameObject p in _waterSplashes)
        //    {
        //        if(p.activeInHierarchy == false)
        //        {
        //            // Controller Shake
        //            //var device = VRDevice.Device;
        //            //var rightHand = device.PrimaryHand;
        //            //var leftHand = device.SecondaryHand;

        //            //var primaryInput = device.PrimaryInputDevice;
        //            //var secondaryInput = device.SecondaryInputDevice;

        //            p.transform.position = new Vector3(_handTransform.position.x, 0.0f, _handTransform.position.z);
        //            p.SetActive(true);
        //            break;
        //        }
        //    }
        //}
    }

}
