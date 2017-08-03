using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growShrink : MonoBehaviour {

    public bool forwardBack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(transform.localScale.x < 1.25)
        {
            if (forwardBack == false)
            {
                transform.localScale = new Vector3(transform.localScale.x + .007f, transform.localScale.y + .007f, 1);
            }
        }
        else
        {
            forwardBack = true;
        }
        if(transform.localScale.x > .85)
        {
            if(forwardBack == true)
            {
                transform.localScale = new Vector3(transform.localScale.x - .007f, transform.localScale.y - .007f, 1);
            }
        }
        else
        {
            forwardBack = false;
        }
		
	}
}
