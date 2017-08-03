using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitDoor : MonoBehaviour {

    public globalVariables globalVars;

    public goToHeaven timeForBed;

	// Use this for initialization
	void Start () {
        globalVars = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        timeForBed = GameObject.Find("timeToLeave").GetComponent<goToHeaven>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.name == "Player")
        {
            if(globalVars.floppyDisks >= 10)
            {
                timeForBed.timeToGoToHeaven = true;
            }
        }
    }

}
