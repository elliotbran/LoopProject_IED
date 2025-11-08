using UnityEngine;
using LoopGame;

public class PhoneActivator : MonoBehaviour
{
    // Referencia al PhoneScript
    public PhoneScript targetPhone;
    
    public GameObject lightToActivate;

    void Start()
    {
        lightToActivate.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar que el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            if (targetPhone != null)
            {
                // 1. Activar el timbre del teléfono
                targetPhone.StartRinging();

                // 2. Activar la luz
                if (lightToActivate != null)
                {
                    lightToActivate.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("Advertencia: No se ha asignado una luz para activar en PhoneActivator.");
                }

                // 3. Desactivar este Trigger para que no se active de nuevo
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Error: La referencia 'targetPhone' en PhoneActivator está vacía. ¡Arrastra el objeto del teléfono!");
            }
        }
    }
}