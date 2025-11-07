using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLoop1 : MonoBehaviour
{
    public GameObject Loop1Object;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(Loop1Object);
            gameObject.SetActive(false);
        }
    }
}
