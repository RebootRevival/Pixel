using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapSpawner : MonoBehaviour {

    public globalVariables globalVar;

    public GameObject[] traps;

    public roomCollider myParent;

    public bool oneSpawn = true;

    public int counter = 0;

	// Use this for initialization
	void Start () {
        myParent = transform.parent.gameObject.GetComponent<roomCollider>();
        globalVar = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        oneSpawn = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (oneSpawn == true)
        {
            Debug.Log("It's a trap!");
            int chooser = Random.Range(0, traps.Length);
            if(Random.Range(0,2) == 1)
            {
                Instantiate(traps[chooser], transform.position, transform.rotation, transform.parent);
            }
            
        }
        counter++;
        if (counter > 2 && transform.name != "realTrap")
        {
            oneSpawn = false;
        }

    }
}
