using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToHeaven : MonoBehaviour {

    public bool timeToGoToHeaven;
    public MeshRenderer myCol;
    public Material plsWork;
    public float countDown;
    public customCharacterController Player;
    public globalVariables blobal;
    // Use this for initialization
    void Start () {
        myCol = gameObject.GetComponent<MeshRenderer>();
        blobal = GameObject.Find("GlobalThings").GetComponent<globalVariables>();
        Player = GameObject.Find("Player").GetComponent<customCharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(timeToGoToHeaven == true)
        {
            blobal.stopMovementForWarp = true;
            countDown += Time.deltaTime;
            //myCol.material.color = new Color(myCol.material.color.r, myCol.material.color.g, myCol.material.color.g, myCol.material.color.a + .1f);
            plsWork.color = new Color(255, 255, 255, plsWork.color.a + .005f);
        }

        if(countDown > 5)
        {
            plsWork.color = new Color(255, 255, 255, 0);
            SceneManager.LoadScene("winningScene");
        }
       
		
	}
}
