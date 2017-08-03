using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floppyKeyScript : MonoBehaviour
{
    public globalVariables globalVar;
    public customCharacterController playa;

    // Use this for initialization
    void Start()
    {
        playa = GameObject.Find("Player").GetComponent<customCharacterController>();
        globalVar = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            playa.chaChing.Play();
            globalVar.floppyDisks += 1;
            Destroy(gameObject);
        }
    }
}
