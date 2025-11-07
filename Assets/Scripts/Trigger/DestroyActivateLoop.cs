using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyActivateLoop: MonoBehaviour
{
    public GameObject destroyLoop;
    public GameObject activateLoop;
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
            Destroy(destroyLoop);
            gameObject.SetActive(false);
            activateLoop.SetActive(true);
        }
    }
}
