using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    public PlayerMove playerMove;
    public ScriptableObject Test;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMove._settings = Test; // Double the speed
        }
    }
}

