using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customCharacterController : MonoBehaviour {

    public globalVariables globals;

    public GameObject gettingHit;

    public Transform myTransform;
    public bool canMove;
    public bool dontGetHit;

    public AudioSource chaChing;
	// Use this for initialization
	void Start () {
        globals = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (canMove && globals.stopMovementForWarp == false)
        {
            dontGetHit = false;
            if (Input.GetKey(KeyCode.W))
            {
                myTransform.position = new Vector3(transform.position.x, transform.position.y + .18f, transform.position.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                myTransform.position = new Vector3(transform.position.x, transform.position.y - .18f, transform.position.z);
            }
            if (Input.GetKey(KeyCode.A))
            {
                myTransform.position = new Vector3(transform.position.x - .18f, transform.position.y, transform.position.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myTransform.position = new Vector3(transform.position.x + .18f, transform.position.y, transform.position.z);
            }
        }
        if(globals.stopMovementForWarp == true && dontGetHit == false)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            dontGetHit = true;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.name == "laser(Clone)" || collision.collider.transform.name == "Saw(Clone)")
        {
            Instantiate(gettingHit, transform.position, transform.rotation);
            globals.health += 4;
        }
        if(collision.collider.transform.name == "Enemy(Clone)")
        {
            Instantiate(gettingHit, collision.transform.position, transform.rotation);
        }
    }
}
