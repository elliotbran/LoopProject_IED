using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCorridor : MonoBehaviour
{
    public Transform TeleportZoneObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("que me esta");

            Vector3 localOffset = transform.InverseTransformPoint(other.transform.position);
            Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation);
            CharacterController cc = other.GetComponent<CharacterController>();

            Debug.Log("Puto holdeando");
            if (cc != null)
            {
                cc.enabled = false;
                other.transform.position = TeleportZoneObject.TransformPoint(localOffset);
                other.transform.localRotation = /*relativeRotation * */TeleportZoneObject.transform.localRotation;
                Debug.Log(other.transform.rotation);
                cc.enabled = true;
                Debug.Log("asd");
            }
        }
    }
}
