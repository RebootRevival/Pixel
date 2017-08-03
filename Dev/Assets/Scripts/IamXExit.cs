using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IamXExit : MonoBehaviour {
    public bool onContact;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            onContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Player")
        {
            onContact = false;
        }
    }
}
