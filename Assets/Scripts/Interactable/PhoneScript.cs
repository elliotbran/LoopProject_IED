using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LoopGame
{
    public class PhoneScript : MonoBehaviour
    {
        bool on;
        [SerializeField] AudioClip onSound;
        [SerializeField] AudioClip offSound;
        [SerializeField] AudioSource audioCall;


        void Start()
        {

        }

        public void UsePhone()
        {
            on = !on;

            if (on)
            {
                audioCall.PlayOneShot(onSound);
            }
            else
            {
                audioCall.PlayOneShot(offSound);
            }
        }
    }
}
