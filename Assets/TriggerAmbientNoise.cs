using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAmbientNoise : MonoBehaviour
{
    [SerializeField] private bool noiseTrigger = false;
    [SerializeField] private bool noise2Trigger = false;
    [SerializeField] private bool noiseFalseTrigger = false;

    public AudioSource Loop2Source;
    public AudioClip noise1;
    public AudioClip noise2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (noiseTrigger)
            {
                Loop2Source.PlayOneShot(noise1, 0.05f);
                gameObject.SetActive(false);
            }

            if (noise2Trigger)
            {
                Loop2Source.PlayOneShot(noise2, 0.05f);
                gameObject.SetActive(false);
            }

            else if (noiseFalseTrigger)
            {
                Loop2Source.Stop();
                gameObject.SetActive(false);
            }
        }
    }
}
