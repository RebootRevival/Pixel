using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class globalVariables : MonoBehaviour {
    public GameObject currentRoom;
    public GameObject myCamera;
    public GameObject Player;
    public IEnumerable allRooms;
    //public GameObject[] allRooms;
    public GameObject startRoom;
    public GameObject warpParticle1;
    public GameObject secondaryParticle;

    public Global_tracker levelRegen;

    public bool inTransition;
    public bool warp;
    public bool stopMovementForWarp;
    public bool onlyOnce;
    public bool turnOffRooms;
    public bool selfDestruct = false;

    public float warpCounter;
    public float enemySpawnRate = 1;
    public float numEnemiesToSpawn;

    public int floppyDisks;
    public int roomCounter;
    public int numRoomsSeen;
    public Text floppies;
    public Text healthAmount;
    public Text gameOver;
    public Text roomsSeen;

    public int health;

    public AudioSource soundSource;
    public AudioClip warpSound;

    // Use this for initialization
    void Start () {
        roomCounter = 0;
        numRoomsSeen = 1;
        floppyDisks = 0;
        levelRegen = GameObject.Find("LevelGen").GetComponent<Global_tracker>();
        warpCounter += 2;
        warpParticle1 = GameObject.Find("warpParticle");
        warpParticle1.SetActive(false);
        secondaryParticle = GameObject.Find("secondaryParticle");
        secondaryParticle.SetActive(false);

        myCamera = GameObject.Find("Main Camera");
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        floppies.text = "Floppys : " + floppyDisks.ToString();
        healthAmount.text = "Assimilation: " + health.ToString() + " % ";
        roomsSeen.text = "Worlds seen: " + numRoomsSeen.ToString();
        if(GameObject.Find("startRoom") == null)
        {
            Application.Quit();
        }
        if(health >= 100)
        {
            health = 100;
            stopMovementForWarp = true;
            Player.GetComponent<mixTheMusic>().gameOver = true;
            gameOver.text = "Assimilation Completed. \n Game Over.";
            GameObject.Find("Player4K01").transform.position = Player.transform.position;
            Player.transform.localScale = new Vector3(Player.transform.lossyScale.x * .9f, Player.transform.lossyScale.y * .9f, Player.transform.lossyScale.z);
            Player.GetComponent<Rigidbody>().isKinematic = true;
        }
        enemySpawnRate += Time.deltaTime * .4f;

        if (Input.GetKey(KeyCode.Space))
        {
            warp = true;
            allRooms = GameObject.FindGameObjectsWithTag("Room");
            enemySpawnRate = 0;
            numEnemiesToSpawn = 0;
        }
        if (numEnemiesToSpawn < 12)
        {
            numEnemiesToSpawn = Mathf.Pow(1.2f, enemySpawnRate);
        }

        if (warp)
        {
            startRoom = GameObject.Find("startRoom");
            warpParticle1.SetActive(true);
            secondaryParticle.SetActive(true);
            foreach(GameObject room in allRooms)
            {
                //room.GetComponent<roomCollider>().enabled = false; 
            }
            if (warpCounter > 0)
            {
                if (soundSource.isPlaying == false && warpCounter > 5)
                {
                    soundSource.Play();
                }
                warpCounter -= Time.deltaTime;
                if(warpCounter < 6)
                {
                    stopMovementForWarp = true;
                    Player.transform.Rotate(new Vector3(0, 0, 10f));
                    Player.GetComponent<Rigidbody>().isKinematic = true;
                }
                if (warpCounter < 6 && warpCounter >= 3)
                {
                    Player.transform.parent = myCamera.transform;
                    Vector3 temp = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, Player.transform.position.z);
                    if ((Player.transform.position.x < float.Epsilon) || (Player.transform.position.x > float.Epsilon))
                    {
                        Player.transform.position = Vector3.MoveTowards(Player.transform.position, temp, .1f);
                    }
                    //if (myCamera.transform.position.x < startRoom.transform.position.x + .5f || myCamera.transform.position.x < startRoom.transform.position.x + .5f)
                    //{
                      myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 8000f), .6f);
                    //}
                }
                else if(warpCounter < 3)
                {
                    startRoom = GameObject.Find("startRoom");
                    roomCounter = 0;
                    if (onlyOnce == false)
                    {
                        foreach (GameObject obj in allRooms)
                        {
                            Destroy(obj);
                        }
                        levelRegen.BuildTileMap();
                        levelRegen.room_counter = 0;
                        onlyOnce = true;
                    }
                    try
                    {
                        startRoom = GameObject.Find("startRoom");
                    }
                    catch
                    {
                        GameObject dumbTry = Instantiate(GameObject.Find("dummyGuy"), new Vector3(transform.position.x, transform.position.x, 4000), transform.rotation);
                    }
                    myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, new Vector3(startRoom.transform.position.x, startRoom.transform.position.y, -10), .51f);
                }
            }
            else
            {
                numRoomsSeen++;
                warpParticle1.SetActive(false);
                secondaryParticle.SetActive(false);
                Player.GetComponent<Rigidbody>().isKinematic = false;
                Player.GetComponent<BoxCollider>().enabled = true;
                onlyOnce = false;
                warp = false;
                allRooms = GameObject.FindGameObjectsWithTag("Room");
                warpCounter = 9;
                inTransition = false;
                stopMovementForWarp = false;
                Player.transform.rotation = Quaternion.identity;
                Player.transform.parent = null;
                myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, -10f);
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -.75f);
                enemySpawnRate = 0;
            }
        }
    }
}
