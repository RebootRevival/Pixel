using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad_cannon : MonoBehaviour {
    public int columns;
    public int rows;
    public int rando;
    public GameObject laser;
    public GameObject left_laser;
    public GameObject right_laser;
    public GameObject up_laser;
    public GameObject down_laser;

    public float countdown=4;

    public globalVariables blobal;

    public roomCollider myParent;

    public bool preShot;


    // Use this for initialization
    void Start () {
        columns = GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().columns;
        rows = GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().rows;
        gameObject.transform.localPosition = new Vector3(0, 0, .02f);
        blobal = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        myParent = transform.parent.gameObject.GetComponent<roomCollider>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myParent.gameObject == blobal.currentRoom)
        {
            countdown = countdown - Time.deltaTime;
            if (countdown < 0)
            {
                preShot = true;
                Destroy(left_laser);
                Destroy(right_laser);
                Destroy(up_laser);
                Destroy(down_laser);
                left_laser = Instantiate(laser, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, .01f), laser.transform.rotation, transform.parent);
                right_laser = Instantiate(laser, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, .01f), laser.transform.rotation, transform.parent);
                up_laser = Instantiate(laser, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, .01f), laser.transform.rotation, transform.parent);
                down_laser = Instantiate(laser, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, .01f), laser.transform.rotation, transform.parent);

                up_laser.transform.Rotate(0, 0, 90f);
                down_laser.transform.Rotate(0, 0, 90f);
                countdown = 5f;
            }
            left_laser.transform.Translate(Vector3.up * Time.deltaTime * 1.8f);
            right_laser.transform.Translate(Vector3.down * Time.deltaTime * 2.2f);
            up_laser.transform.Translate(Vector3.up * Time.deltaTime * 1.8f);
            down_laser.transform.Translate(Vector3.down * Time.deltaTime * 2.2f);
        }
        else
        {
            if (preShot)
            {
                Destroy(left_laser);
                Destroy(right_laser);
                Destroy(up_laser);
                Destroy(down_laser);
                preShot = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.transform.name == "Player")
        {
            if(blobal.warp == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
