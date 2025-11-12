using UnityEngine;
using LoopGame;

public class GramophoneActivator : MonoBehaviour
{
    public AudioSource gramophone;


    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar que el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            if (gramophone != null)
            {
                gramophone.Play();

                gameObject.SetActive(false);
            }
        }
    }
}