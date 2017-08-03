using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitDetect : MonoBehaviour {

    public enemyAI myParent;

	// Use this for initialization
	void Start () {
        myParent = transform.parent.gameObject.GetComponent<enemyAI>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name == "Player")
        {
            myParent.readyToMove = true;
        }
        else if (other.gameObject.transform.name == "Enemy")
        {

        }
        else if (other.transform.name == "Room")
        {
  
        }
    }
}
