using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivatePlayableDirector : MonoBehaviour
{

    public PlayableDirector playableDirector;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
