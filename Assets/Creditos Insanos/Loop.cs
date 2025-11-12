using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loop : MonoBehaviour
{
    public GameObject BotonAAA;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoopCreditos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoopCreditos()
    {
        yield return new WaitForSeconds(40f);
        BotonAAA.SetActive(true);
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
