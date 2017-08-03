using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomCollider : MonoBehaviour {
    public globalVariables globalVar;

    public GameObject centerObject;
    public GameObject xExitObject;
    public GameObject spawnerObject;
    public GameObject trapSpawnerObject;
    public GameObject exitDoor;

    public bool xExit;
    public bool moveRooms;
    public bool onlySpawnEnemiesOnce;
    public bool thereAreEnemiesHere;
    public bool thereAreTrapsHere;
    public bool oldRoom;

    public IamXExit theExit;
    public enemySpawn mySpawners;
    public trapSpawner myTraps;

    public Transform myCamera;

    public customCharacterController myChar;

    public Vector3 velocity = Vector3.zero;
    public Vector3 myV;
    public Vector3 myCharV;
    public Vector3 cameraV;

    public float roomTransCounter;
    public int firstFrame;

    public float thisSucks;
    public float characterTransSpeedX;
    public float characterTransSpeedY;

    // Use this for initialization
    void Start () {
        exitDoor = GameObject.Find("exitDoor");
        
        spawnerObject = GameObject.Find("EnemySpawner");
        trapSpawnerObject = GameObject.Find("trapSpawner");
        if(thereAreEnemiesHere == true)
        {
            Vector3 temp = new Vector3(transform.position.x, transform.position.y, -3);
            spawnerObject = Instantiate(spawnerObject, temp, transform.rotation, transform);
            mySpawners = spawnerObject.GetComponent<enemySpawn>();
            mySpawners.enabled = true;
        }
        if(thereAreTrapsHere == true)
        {
            Vector3 temp = new Vector3(transform.position.x, transform.position.y, -3);
            trapSpawnerObject = Instantiate(trapSpawnerObject, temp, transform.rotation, transform);
            myTraps = trapSpawnerObject.GetComponent<trapSpawner>();
            myTraps.enabled = true;
        }
        globalVar = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        centerObject = gameObject;
        
        //myParent = centerObject.GetComponent<roomColliderParent>();
        roomTransCounter = 0;
        myCamera = GameObject.Find("Main Camera").gameObject.transform;
        myChar = GameObject.Find("Player").GetComponent<customCharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(firstFrame == 0)
        {
            if (transform.name == "exitRoom")
            {
                Vector3 tempVec = new Vector3(transform.position.x, transform.position.y, -5f);
                Instantiate(exitDoor, tempVec, transform.rotation, transform);
                Destroy(transform.GetComponentInChildren<trapSpawner>().gameObject);
            }
            xExitObject = transform.GetChild(0).gameObject;
            //mySpawners = transform.GetComponentInChildren<enemySpawn>();
            theExit = xExitObject.GetComponent<IamXExit>();
        }
        if(firstFrame >= 3)
        {
            if (transform.name == "exitRoom")
            {
                foreach (Transform child in transform)
                {
                    if (child.name == "realTrap(Clone)")
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
        }
        firstFrame++;
        //Enemy Spawn
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //if(globalVar.currentRoom == gameObject && onlySpawnEnemiesOnce == false && thereAreEnemiesHere == true)
        //{
            
            
        //}
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //Camera Transition
        //----------------------------------------------------------------------------------------------------------------------------------------------
		if(moveRooms == true)
        {
            if (onlySpawnEnemiesOnce == false)
            {
                GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject work in tempArray)
                {
                    if (work.transform.name != "Enemy")
                    {
                        Destroy(work);
                    }
                }
                mySpawners.spawn = true;
                onlySpawnEnemiesOnce = true;
            }
            xExit = theExit.onContact;
            myChar.canMove = false;
            roomTransCounter += Time.deltaTime;
            cameraV = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, -10f);
            myV = new Vector3(centerObject.transform.position.x, centerObject.transform.position.y, myCamera.transform.position.z);
            myCamera.position = Vector3.LerpUnclamped(cameraV, myV, thisSucks);
            myV.z = myChar.gameObject.transform.position.z;
            myCharV = new Vector3(myChar.gameObject.transform.position.x, myChar.gameObject.transform.position.y, myChar.gameObject.transform.position.z);
            if (xExit == true)
            {
                myChar.gameObject.transform.position = Vector3.Lerp(myCharV, myV, characterTransSpeedX);
            }
            else
            {
                myChar.gameObject.transform.position = Vector3.Lerp(myCharV, myV, characterTransSpeedY);
            }
        }
        if(roomTransCounter > 1.5)
        {
            moveRooms = false;
            myChar.canMove = true;
            roomTransCounter = 0;
            globalVar.inTransition = false;
            onlySpawnEnemiesOnce = false;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------
        if(globalVar.warp == true && globalVar.turnOffRooms == true)
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player")
        {
            moveRooms = true;
            globalVar.inTransition = true;
            globalVar.currentRoom = gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Player")
        {
            moveRooms = false;
            globalVar.selfDestruct = true;
            globalVar.roomCounter += 1;
            /*if (thereAreEnemiesHere == true)
            {
                Debug.Log("I'm HERE");
                mySpawners.destroyEnemies = true;
                onlySpawnEnemiesOnce = false;
            }*/
        }
        else
        {
            //Destroy(other.gameObject);
        }
    }
}
