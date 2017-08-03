using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour {
    public globalVariables globalVar;
    public roomCollider myParent;

    public Collider[] neighbours;

    public bool spawn;
    public bool destroyEnemies;
    public bool foundProperLocation;

    public GameObject[] enemyToSpawn;
    public GameObject[] curEnemies;
    public GameObject spawnLocation;
    public GameObject Player;

    public int chooser;

    public float spawnCircleSize;
    public float distanceToP = 0;
    public float spawnLocationSize;
    public float enemySize;
    public float distanceBetweenEnemies;

    public LayerMask enemyLayer;

    public Vector2 insideCircle; //Randomize the location of enemies inside a unit circle around where the spawner is located
    public Vector2 locationInsideCircle; //The location in the unit circle where the spawner will be located
    public Vector3 placeToPlace;
    public Vector3 newPos1;
    // Use this for initialization

    public Vector3 FindNewPos(int counter)
    {
        int dontDie = 0;

        do
        {
            dontDie++;
            newPos1 = new Vector3(Random.insideUnitCircle.x * enemySize + spawnLocation.transform.position.x, Random.insideUnitCircle.y * enemySize + spawnLocation.transform.position.y, -.5f);
            neighbours = Physics.OverlapSphere(newPos1, distanceBetweenEnemies, enemyLayer);
            foreach(Collider obj in neighbours)
            {
                Debug.Log(obj.gameObject.name);
            }
            distanceToP = Vector3.Distance(Player.transform.position, newPos1);
        }while((neighbours.Length > 0 && distanceToP < 5) && dontDie < 100);

        return newPos1;
    }
	void Start () {
        Player = GameObject.Find("Player");
        Debug.Log(LayerMask.LayerToName(8));
        myParent = GetComponentInParent<roomCollider>();
        globalVar = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        spawnLocation = transform.GetChild(0).gameObject;
        locationInsideCircle = Random.insideUnitCircle * spawnLocationSize;
        spawnLocation.transform.position = new Vector3(spawnLocation.transform.position.x + locationInsideCircle.x, spawnLocation.transform.position.y + locationInsideCircle.y, -5f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (spawn && globalVar.roomCounter > 0)
        {
            for (int x = 0; x < Mathf.RoundToInt(globalVar.numEnemiesToSpawn); x++)
            {
                chooser = Random.Range(0, enemyToSpawn.Length);
                insideCircle = Random.insideUnitCircle * spawnCircleSize;
                placeToPlace = FindNewPos(x);
                curEnemies[x] = Instantiate(enemyToSpawn[chooser], placeToPlace, transform.rotation, transform) as GameObject;
                curEnemies[x].GetComponent<enemyAI>().enabled = true;
            }
            spawn = false;
        }
        if (globalVar.selfDestruct)
        {
            foreach(Transform child in transform)
            {

                //if (child.name != ("spawnLocation"))
                //{
                    //Destroy(child.gameObject);
                //}
            }
            globalVar.selfDestruct = false;
            destroyEnemies = false;
        }
	}
}
