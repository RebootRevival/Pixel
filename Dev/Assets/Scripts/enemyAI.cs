using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {
    public globalVariables globalVar;

    public GameObject currentRoom;
    public GameObject Player;
    public GameObject myPartSys;

    public Collider[] neighbours;
    public goToHeaven readyToEnd;

    public float maxSpeed;

    public bool readyToMove;
    public bool once;
    public bool upDown;

    public Animator myAnime;
    public Animation myAnimtion;
    public Animation myAnimtionDown;

    /*public Collider[] checkNeigbors(Vector3 newPos, float mySize, LayerMask myLayer)
    {
        //do
        //{
        neighbours = Physics.OverlapSphere(transform.position, mySize * 1.2f, myLayer);
        return neighbours;
    }*/

    // Use this for initialization
    void Start () {
        readyToEnd = GameObject.Find("timeToLeave").GetComponent<goToHeaven>();
        myAnimtion = gameObject.GetComponent<Animation>();
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, .001f, 0));
        if(transform.name == "Enemy")
        {
            this.enabled = false;
        }
        globalVar = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        Player = GameObject.Find("Player");
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (globalVar.inTransition == false && globalVar.stopMovementForWarp == false) //&& globalVar.currentRoom == currentRoom)
        {
            if(readyToMove == true)
            {
                if(once == false)
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    once = true;
                }
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, maxSpeed);
                if (Player.transform.position.y < transform.position.y && !upDown)
                {
                    Debug.Log("Below");
                    //myAnime.SetTrigger("Switch");
                    myAnime.SetTrigger("EnemyWalking");
                    upDown = true;
                }
                if (Player.transform.position.y > transform.position.y && upDown)
                {
                    upDown = false;
                    Debug.Log("Above");
                    //myAnime.SetTrigger("Switch");
                    myAnime.SetTrigger("EnemyWalkAway");
                }
                
            }
        }
        if(globalVar.warpCounter < 3 || globalVar.selfDestruct == true)
        {
            //Destroy(gameObject);
        }

	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.transform.name == "Player")
        {
            globalVar.health += 10;
        }
        if(collision.collider.transform.name == "laser(Clone)" || collision.collider.transform.name == "Saw(Clone)")
        {
            myPartSys.SetActive(true);
            myPartSys.transform.parent = null;
            myPartSys.transform.localScale = new Vector3(myPartSys.transform.localScale.x, myPartSys.transform.localScale.y, myPartSys.transform.localScale.z / 2f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name == "Player")
        {
            
        }
        else if(other.gameObject.transform.name == "Enemy")
        {

        }
        else if(other.transform.name == "Room")
        {
            
        }
    }

}
