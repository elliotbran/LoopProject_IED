/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camlookat : MonoBehaviour
{
    public Transform cam, player;

    void Update()
    {
        cam.LookAt(player);
    }
}*/
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    public Transform shoebill;
    public Transform player;
    public bool prueba;

    void Update()
    {

            // Get direction toward player
            Vector3 direction = player.position - shoebill.position;

            // Calculate the rotation that looks at the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Get the current rotation in Euler angles
            Vector3 currentEuler = shoebill.rotation.eulerAngles;

            // Apply only the X rotation from the look rotation
            shoebill.rotation = Quaternion.Euler(lookRotation.eulerAngles.x, currentEuler.y, currentEuler.z);
        

    }
}
