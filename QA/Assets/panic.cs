using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panic : MonoBehaviour {
    public bool forwardBack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x > 4.4)
        {
            if (forwardBack == true)
            {
                transform.localScale = new Vector3(transform.localScale.x - .007f, transform.localScale.y - .007f, 1);
            }
        }
        else
        {
            forwardBack = false;
        }
        if (transform.localScale.x < 4.9)
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
    }
}
