using LoopGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinternaScript : MonoBehaviour
{
    public GameObject Linterna;
    public GameObject LuzLinterna;
    public GameObject LuzLinternaBase;
    public GameObject textoTutorial;
    DoorScript DoorScript;

    public GameObject AudioFix;
    AudioSource audiosource;

    private void Start()
    {
        DoorScript = FindAnyObjectByType<DoorScript>();
        audiosource = AudioFix.GetComponent<AudioSource>();
    }
    public void Pickup()
    {
        audiosource.enabled = true;
        DoorScript.islocked = false;
        LuzLinternaBase.SetActive(true);  
        StartCoroutine(TextoTutorial());
    }
     IEnumerator TextoTutorial()
    {
        Linterna.SetActive(false);
        LuzLinterna.SetActive(true);
        textoTutorial.SetActive(true);
        yield return new WaitForSeconds(3);
        textoTutorial.SetActive(false);
    }
}
