using LoopGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopGame
{
    public class GetKey : MonoBehaviour
    {
        public GameObject key;        
        public DoorScript doorScript;
        public GameObject loop3Trigger;

        // Start is called before the first frame update
        void Start()
        {
            doorScript.isOpen = false;
        }
        // Update is called once per frame
        void Update()
        {
        }
        public void GotKey()
        {
            key.SetActive(false);
            loop3Trigger.SetActive(true);
            doorScript.isOpen = true;
        }
    }
}


