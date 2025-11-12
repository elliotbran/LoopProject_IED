using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate5_8Triggers : MonoBehaviour
{

    public GameObject Triggers5_8;

    private void OnTriggerEnter(Collider other)
    {
        Triggers5_8.SetActive(true);
    }

}
