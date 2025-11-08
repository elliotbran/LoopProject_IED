using System.Collections;
using UnityEngine;

namespace LoopGame
{
    // CLASE PRINCIPAL: PhoneScript (Adjunto al objeto interactivo del teléfono)
    public class PhoneScript : MonoBehaviour
    {
        bool on;
        private bool isRinging = false;

        [Header("Referencias")]
        public GameObject phoneInteractionObject;
        public GameObject doll;

        [Header("Sonidos")]
        public AudioClip onSound;
        public AudioClip offSound;
        public AudioClip ringSound;
        public AudioClip callSound;
        public AudioClip endCallSound;
        public AudioSource audioCall;

        private Collider phoneCollider;
        private Coroutine callCoroutine;

        // Llamado por el PhoneActivator externo al detectar al jugador.
        public void StartRinging()
        {
            if (isRinging) return;

            isRinging = true;
            Debug.Log("Activado: ¡El teléfono comienza a sonar!");

            if (ringSound != null && audioCall != null)
            {
                audioCall.clip = ringSound;
                audioCall.loop = true;
                audioCall.Play();
            }
        }

        void Start()
        {
            doll.SetActive(false);
            phoneCollider = GetComponent<Collider>();
        }

        public void UsePhone()
        {
            if (!isRinging) return;

            on = !on;

            if (on) // DESCOLGAR
            {
                if (audioCall.isPlaying && audioCall.clip == ringSound)
                {
                    audioCall.Stop();
                }

                if (onSound != null)
                {
                    callCoroutine = StartCoroutine(StartCallSequence());
                }
            }
            else // COLGAR
            {
                if (callCoroutine != null)
                {
                    StopCoroutine(callCoroutine);
                    callCoroutine = null;
                }

                audioCall.Stop();
                audioCall.clip = null;
                isRinging = false;

                if (offSound != null)
                {
                    StartCoroutine(PlayOffSoundAndDisableInteraction());
                }
                else if (phoneInteractionObject != null)
                {
                    phoneInteractionObject.SetActive(false);
                }
            }
        }

        IEnumerator StartCallSequence()
        {
            audioCall.PlayOneShot(onSound);
            yield return new WaitForSeconds(onSound.length);

            if (on && callSound != null)
            {
                audioCall.clip = callSound;
                audioCall.loop = false;
                audioCall.Play();

                yield return new WaitForSeconds(callSound.length);

                if (on && endCallSound != null)
                {
                    audioCall.PlayOneShot(endCallSound);
                }
            }
        }

        IEnumerator PlayOffSoundAndDisableInteraction()
        {
            audioCall.PlayOneShot(offSound);
            yield return new WaitForSeconds(offSound.length);

            if (phoneInteractionObject != null)
            {
                phoneInteractionObject.SetActive(false);
                doll.SetActive(true);
            }
        }
    }
}