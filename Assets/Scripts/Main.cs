﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {


	public int mapsize = 40;                    // Planning to have GLOBAL variables 
    GameObject player;
    public Transform playerTrans;   			//  here. AY-AY Sir!
    public Animator playerAnim;
    public Rigidbody2D playerRigi;
    public movePlayer playerMove;
	                                            // Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
        playerTrans = player.GetComponent<Transform>();
        playerAnim  = player.GetComponent<Animator>();
        playerRigi  = player.GetComponent<Rigidbody2D>();
        playerMove  = player.GetComponent<movePlayer>();

    }

    // Update is called once per frame
    void Update () {

	}
		
	void restart() {

		Debug.Log ("Restarting game");
	}

	void save() {
		Debug.Log ("Saving game...");
	}

	void load(string filename){
		Debug.Log ("Loading game...");
	}

														// Adjusts an input vector to follow the curve of a 
														// circle, using the angle.
	public Vector2 diagonalCompensate(Vector2 inputVec) { 

        float angle = Vector2.Angle( Vector2.right, inputVec ) * Mathf.Deg2Rad;
        												// in two cases we have to make the angle negative

        Debug.Log("Angle: " + angle);

        if (inputVec.x < 0 && inputVec.y != 0 ){
        	angle -= Mathf.PI / 2;
        } else if (inputVec.x < 0) {
        	angle -= Mathf.PI;
        }

		return new Vector2( inputVec.x * Mathf.Cos(angle), inputVec.y * Mathf.Sin(angle) );
	}
}








