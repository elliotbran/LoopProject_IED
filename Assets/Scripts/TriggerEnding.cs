using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerEnding : MonoBehaviour
{
    public PlayableDirector endingCutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endingCutscene.enabled = true;
        }
    }
}
