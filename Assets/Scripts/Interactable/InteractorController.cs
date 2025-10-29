using EtherealTerror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LoopGame
{
    public class InteractorController : MonoBehaviour
    {
        public GameObject interactText;

        void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
            {
                if (hit.collider.tag != "Interactable")
                {
                    interactText.SetActive(false);
                }
                else
                {
                    interactText.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<InteractableObject>().onInteract.Invoke();
                        interactText.SetActive(false);
                    }

                }
            }
            else
            {
                interactText.SetActive(false);
            }
        }
    }

}
