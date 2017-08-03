using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixTheMusic : MonoBehaviour {

    public AudioClip lowRez;
    public AudioClip medRez;
    public AudioClip highRez;

    public AudioSource lowRezSource;
    public AudioSource medRezSource;
    public AudioSource highRezSource;
    public AudioSource gameOverMusic;

    public Global_tracker checkTheRez;

    public bool gameOver;

	// Use this for initialization
	void Start () {
        checkTheRez = GameObject.Find("LevelGen").GetComponent<Global_tracker>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameOver == false) {
            if (checkTheRez.Rez == 'L')
            {
                if (lowRezSource.volume < .6f)
                {
                    lowRezSource.volume += .01f;
                }
                if (medRezSource.volume > 0)
                {
                    medRezSource.volume -= .01f;
                }
                if (highRezSource.volume > 0)
                {
                    highRezSource.volume -= .01f;
                }
            }
            if (checkTheRez.Rez == 'M')
            {
                if (lowRezSource.volume > 0)
                {
                    lowRezSource.volume -= .01f;
                }
                if (medRezSource.volume < .6f)
                {
                    medRezSource.volume += .01f;
                }
                if (highRezSource.volume > 0)
                {
                    highRezSource.volume -= .01f;
                }
            }
            if (checkTheRez.Rez == 'H')
            {
                if (lowRezSource.volume > 0)
                {
                    lowRezSource.volume -= .01f;
                }
                if (medRezSource.volume > 0)
                {
                    medRezSource.volume -= .01f;
                }
                if (highRezSource.volume < .6f)
                {
                    highRezSource.volume += .01f;
                }
            }
        }
        else
        {
            if (lowRezSource.volume > 0)
            {
                lowRezSource.volume = 0;
            }
            if (medRezSource.volume > 0)
            {
                medRezSource.volume = 0;
            }
            if (highRezSource.volume > 0)
            {
                highRezSource.volume  = 0;
            }
            gameOverMusic.Play();
            this.enabled = false;
        }
        


    }
}
