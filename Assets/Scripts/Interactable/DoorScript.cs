using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

namespace LoopGame
{
    public class DoorScript : MonoBehaviour
    {
        
        private bool doorOpen;
        private float cooldown;
        public bool isOpen;
        private void Start()
        {
            doorOpen = false;
        }

        public void UseDoor()
        {
            if (isOpen!)
            {
                if (cooldown > 0)
                {
                    return;
                }

                cooldown = 1;

                doorOpen = !doorOpen;

                if (doorOpen)
                {
                    
                }
                else
                {

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