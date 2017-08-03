using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_check : MonoBehaviour {
    
    // Use this for initialization
    void Start ()
    {
        

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public void IHaveBeenHit()
    {
        RaycastHit hit;
       // Debug.Log("I'M IN THE FUNCTION");
        if (Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 100.0f))
        {
            if(hit.transform.tag == "Raycaster")
            {
                //Debug.Log("I WAS HIT");
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
            if (hit.transform.parent.name == "exitRoom")
            {
                //Debug.Log("Exit was hit");
                GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().exit_path = true;
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
        }

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 100.0f))
        {
            if (hit.transform.tag == "Raycaster")
            {
               // Debug.Log("I WAS HIT");
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
            if (hit.transform.parent.name == "exitRoom")
            {
                //Debug.Log("Exit was hit");
                GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().exit_path = true;
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
        }

        if (Physics.Raycast(gameObject.transform.position, Vector3.right, out hit, 100.0f))
        {
            if (hit.transform.tag == "Raycaster")
            {
               // Debug.Log("I WAS HIT");
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
            if (hit.transform.parent.name == "exitRoom")
            {
                //Debug.Log("Exit was hit");
                GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().exit_path = true;
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
        }

        if (Physics.Raycast(gameObject.transform.position, Vector3.left, out hit, 100.0f))
        {
            if (hit.transform.tag == "Raycaster")
            {
              //  Debug.Log("I WAS HIT");
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
            if (hit.transform.parent.name == "exitRoom")
            {
              //  Debug.Log("Exit was hit");
                GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().exit_path = true;
                hit.transform.GetComponent<Ray_check>().IHaveBeenHit();
            }
        }

    }
}
