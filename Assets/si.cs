using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class si : MonoBehaviour
{
    public bool luz = false;
    public GameObject luz2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(luz == false)
        {
            StartCoroutine(Hola());
        }
    }
    public IEnumerator Hola()
    {
        luz = true;
        yield return new WaitForSeconds(Random.Range(2,7));
        luz2.SetActive(false);
        yield return new WaitForSeconds(Random.Range(0.2f,0.5f));
        luz2.SetActive(true);
       yield return new WaitForSeconds(Random.Range(0.2f,0.7f));
        luz2.SetActive(false);
        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        luz2.SetActive(true);
        luz = false;
    }
}
