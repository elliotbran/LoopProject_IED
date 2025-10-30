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

        // Variable para almacenar la Coroutine y poder detenerla
        private Coroutine callCoroutine;

        void Start()
        {
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
            else // COLGAR (Interrumpe la llamada)
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

                // Reproducir offSound (sonido de colgar/finalizar la interacción)
                if (offSound != null)
                {
                    audioCall.PlayOneShot(offSound);
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

                // Opcional: Podrías querer poner 'on = false' aquí si la llamada termina por sí misma.
                // on = false;
            }
        }
    }
}