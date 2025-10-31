using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LoopGame
{
    public class InteractorController : MonoBehaviour
    {
        public GameObject interactText;

        // 1. Nueva variable para la distancia de interacción, visible en el Inspector.
        [SerializeField] private float interactionDistance = 3.0f;

        void Update()
        {
            RaycastHit hit;

            // 2. Usar la variable interactionDistance en el Raycast.
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
            {
                // Verifica si el objeto golpeado tiene la etiqueta "Interactable"
                if (hit.collider.CompareTag("Interactable"))
                {
                    // Objeto interactuable a la vista
                    interactText.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Llama a la función de interacción y oculta el texto
                        var interactable = hit.collider.GetComponent<InteractableObject>();
                        if (interactable != null)
                        {
                            interactable.onInteract.Invoke();
                            interactText.SetActive(false);
                        }
                    }
                }
                else
                {
                    // Objeto golpeado pero no es interactuable
                    interactText.SetActive(false);
                }
            }
            else
            {
                // El Raycast no golpeó nada dentro de la distancia límite
                interactText.SetActive(false);
            }
        }
    }

}