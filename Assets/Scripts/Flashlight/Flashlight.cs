using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    private Vector3 OffsetVect3; // the offset

    public GameObject FollowCam; // the camera

    [SerializeField] private float MoveSpeed = 13f; // speed of movement

    public Light flashLight; // the flashlight

    private bool flashlightisOn = false; // is the flashlight on or off

    // audio

    public AudioSource Source; // play audio

    public AudioClip flashlightOnSound;

    public AudioClip flashlightOffSound;

    // Start is called before the first frame update
    void Start()
    {
        flashLight.enabled = false; // flashlight is off at start
        OffsetVect3 = transform.position - FollowCam.transform.position; // calculate the offset

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = FollowCam.transform.position + OffsetVect3; // follow the camera with the offset

        transform.rotation = Quaternion.Slerp(transform.rotation, FollowCam.transform.rotation, MoveSpeed * Time.deltaTime); // change rotation with offset

        if(Input.GetKeyDown(KeyCode.F)) // if F is pressed
        {
           if (flashlightisOn == false)
            {
                flashLight.enabled = true;
                flashlightisOn = true;

                // audio

                Source.PlayOneShot(flashlightOnSound);
            }

             else if (flashlightisOn == true)
            {
                 flashLight.enabled = false;
                 flashlightisOn = false;
    
                 // audio
    
                 Source.PlayOneShot(flashlightOffSound);
            }
        }

    }
}
