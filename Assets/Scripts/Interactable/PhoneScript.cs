using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace LoopGame
{
    public class PhoneScript : MonoBehaviour
    {
        bool on;
        [SerializeField] AudioClip onSound;
        [SerializeField] AudioClip offSound;
        [SerializeField] AudioClip ringSound;
        [SerializeField] AudioClip callSound;
        [SerializeField] AudioClip endCallSound;
        [SerializeField] AudioSource audioCall;

        // Referencia al BoxCollider que queremos desactivar
        private BoxCollider phoneCollider;

        // Variable para almacenar la Coroutine y poder detenerla
        private Coroutine callCoroutine;

        void Start()
        {
            // OBTENER EL BOXCOLLIDER: Asume que el BoxCollider está en el mismo GameObject.
            phoneCollider = GetComponent<BoxCollider>();

            // 1. Reproducir ringSound loopeado al inicio.
            if (ringSound != null && audioCall != null)
            {
                audioCall.clip = ringSound;
                audioCall.loop = true; // Solo ringSound está en bucle
                audioCall.Play();
            }
        }

        public void UsePhone()
        {
            on = !on;

            if (on) // DESCOLGAR (Inicia la llamada)
            {
                // Parar ringSound que estaba en loop
                if (audioCall.isPlaying && audioCall.clip == ringSound)
                {
                    audioCall.Stop();
                }

                if (onSound != null)
                {
                    // Iniciar la secuencia de la llamada en una Coroutine
                    callCoroutine = StartCoroutine(StartCallSequence());
                }
            }
            else // COLGAR (Interrumpe la llamada y desactiva el collider)
            {
                // Detener cualquier Coroutine de llamada activa para interrumpir la secuencia
                if (callCoroutine != null)
                {
                    StopCoroutine(callCoroutine);
                    callCoroutine = null;
                }

                // Detener el audio actual (onSound o callSound)
                audioCall.Stop();
                audioCall.clip = null;

                // INICIAR COROUTINE para reproducir offSound y luego desactivar el Collider.
                if (offSound != null)
                {
                    StartCoroutine(PlayOffSoundAndDisableCollider());
                }
                else if (phoneCollider != null)
                {
                    // Si no hay offSound, desactivar inmediatamente (como fallback)
                    phoneCollider.enabled = false;
                }
            }
        }

        // Coroutine para manejar la secuencia: onSound -> callSound (una vez) -> endCallSound (al terminar callSound)
        IEnumerator StartCallSequence()
        {
            // 1. Reproducir onSound
            audioCall.PlayOneShot(onSound);

            // Esperar la duración de onSound
            yield return new WaitForSeconds(onSound.length);

            // 2. Reproducir callSound una sola vez (si el teléfono sigue "on")
            if (on && callSound != null)
            {
                audioCall.clip = callSound;
                audioCall.loop = false; // callSound NO está en bucle
                audioCall.Play();

                // Esperar a que callSound termine de sonar
                yield return new WaitForSeconds(callSound.length);

                // 3. Cuando callSound ACABA, reproducir endCallSound (si el teléfono sigue "on")
                if (on && endCallSound != null)
                {
                    audioCall.PlayOneShot(endCallSound);
                }
            }
        }

        // NUEVA COROUTINE: Reproduce offSound y luego desactiva el BoxCollider.
        IEnumerator PlayOffSoundAndDisableCollider()
        {
            // Reproducir offSound
            audioCall.PlayOneShot(offSound);

            // Esperar la duración de offSound
            yield return new WaitForSeconds(offSound.length);

            // Desactivar el BoxCollider para evitar más interacciones
            if (phoneCollider != null)
            {
                phoneCollider.enabled = false;
            }
        }
    }
}