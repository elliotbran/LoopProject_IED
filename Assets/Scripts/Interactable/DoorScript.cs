using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoopGame
{
    public class DoorScript : MonoBehaviour
    {
        [SerializeField] Animation anim;
        [SerializeField] AudioClip openSound;
        [SerializeField] string doorOpenAnimation;
        [SerializeField] string doorCloseAnimation;
        private bool doorOpen;
        private float cooldown;
        private AudioSource audioSource;
        public bool islocked;

        private void Start()
        {
            doorOpen = false;
            islocked = true;
            audioSource = GetComponent<AudioSource>();
        }

        public void UseDoor()
        {
            if (islocked!)
            {
                if (cooldown > 0)
                {
                    return;
                }

                cooldown = 1;

                doorOpen = !doorOpen;

                if (doorOpen)
                {
                    anim.Play(doorOpenAnimation);
                    audioSource.PlayOneShot(openSound);
                }
                else
                {
                    anim.Play(doorCloseAnimation);
                }
            }
            else
            {
                Debug.Log("Door is locked");
            }
        }

        private void FixedUpdate()
        {
            cooldown -= Time.deltaTime;
        }
    }
}