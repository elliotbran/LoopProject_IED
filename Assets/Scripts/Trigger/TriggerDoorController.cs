using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    public AudioSource doorSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                doorSource.PlayOneShot(doorOpen, 0.4f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
                doorSource.PlayOneShot(doorClose, 0.4f);  
                gameObject.SetActive(false);
            }
        }
    }
}
