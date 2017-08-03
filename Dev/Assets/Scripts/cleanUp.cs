using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanUp : MonoBehaviour {

    public GameObject[] badItems;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        badItems = GameObject.FindGameObjectsWithTag("Trap");

        foreach(GameObject thing in badItems)
        {
            if(thing.transform.parent == null)
            {
                Destroy(thing);
            }
        }
	}
}
