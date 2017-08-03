using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    public globalVariables vars;
    public int columns;
    public int rows;
    public int rando;
    public int counter;
    public int animationCounter;

    public Vector3 initialdirection;

    public Sprite[] animationFrames;
    public SpriteRenderer mySprite;
    
	// Use this for initialization
	void Start () {
        vars = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        columns = GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().columns;
        rows = GameObject.FindGameObjectWithTag("Global").GetComponent<Global_tracker>().rows;
        rando = Random.Range(0, 5);
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        animationCounter = 0;
        
        if(rando == 0)
        {
            initialdirection = Vector3.up * 2; 
        }
        else if(rando == 1)
        {
            initialdirection = Vector3.down * 2;
        }
        else if(rando == 2)
        {
            initialdirection = Vector3.left * 2;
        }
        else
        {
            initialdirection = Vector3.right * 2;
        }

        rando = Random.Range(0, 2);
        if(rando == 0)
        {
            rando = -1;
        }
        else
        {
            rando = 1;
        }
        gameObject.transform.localPosition = new Vector3(Random.Range(0, rando * columns/2), Random.Range(0, rando * rows/2), .02f);


    }

    // Update is called once per frame
    void FixedUpdate () {
        if (transform.parent.gameObject == vars.currentRoom)
        {
            if (gameObject.GetComponent<AudioSource>().isPlaying == false)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
        counter++;
        transform.Translate(initialdirection * Time.deltaTime);

        mySprite.sprite = animationFrames[animationCounter];

        if (counter%5 == 0)
        {
            animationCounter++;
        }
        
        if(animationCounter == 4)
        {
            animationCounter = 0;
        }
		
	}

    private void OnCollisionEnter(Collision other)
    {
        initialdirection = -initialdirection;
    }


}
