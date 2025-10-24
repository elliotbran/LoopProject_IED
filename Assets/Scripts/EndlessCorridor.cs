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
            Debug.Log("Entrando al teletransporte...");

            // Calcula la posición del jugador relativa al portal actual
            Vector3 localOffset = transform.InverseTransformPoint(other.transform.position);

            // Calcula la rotación relativa entre ambos portales
            Quaternion relativeRotation = TeleportZoneObject.rotation * Quaternion.Inverse(transform.rotation);

            // Si el jugador tiene Rigidbody, lo usamos para moverlo con seguridad
            Rigidbody rb = other.attachedRigidbody;

            if (rb != null)
            {
                // Desactiva momentáneamente la física para evitar empujones raros
                rb.isKinematic = true;
                rb.position = TeleportZoneObject.TransformPoint(localOffset);
                rb.rotation = relativeRotation * rb.rotation;
                rb.isKinematic = false;
            }
            else
            {
                // Si no tiene Rigidbody, simplemente movemos el transform
                other.transform.position = TeleportZoneObject.TransformPoint(localOffset);
                other.transform.rotation = relativeRotation * other.transform.rotation;
            }

            Debug.Log("Teletransporte completado.");
        }
    }
}
