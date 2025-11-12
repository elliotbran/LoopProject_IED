using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDollSpawn : MonoBehaviour
{
    public GameObject DemonDoll;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DemonDoll.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

