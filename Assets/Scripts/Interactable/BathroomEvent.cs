using UnityEngine;

public class BathroomEvent : MonoBehaviour
{
    // Referencia: Arrastra el objeto Puerta aquí en el Inspector.
    public KickedDoor puertaObjetivo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (puertaObjetivo != null)
            {
                puertaObjetivo.ActivarGolpes();
            }
        }
    }
}