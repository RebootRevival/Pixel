using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning_lazer : MonoBehaviour {
    public int columns;
    public int rows;
    public GameObject laser;
    public int rotate_speed=5;
    public int rando;
    
	// Use this for initialization
	void Start () {
        columns = GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().columns;
        rows = GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().rows;
        gameObject.transform.localPosition = new Vector3(Random.Range(0, (columns/2)-2), Random.Range(0, rows/2), .02f);
        Instantiate(laser, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1, .01f), laser.transform.rotation, gameObject.transform);
        rando = Random.Range(0, 2);
        if(rando == 0)
        {
            rando = -1;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        gameObject.transform.Rotate(0, 0, rando * rotate_speed);
	}
}
