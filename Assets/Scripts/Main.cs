﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;    // List<type>()
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public int mapsize = 40;
    public bool mouseOn = false;      // mouse on / off

    public Transform     playerTrans;           //  here. AY-AY Sir!
    public Animator      playerAnim;
    public Rigidbody2D   playerRigi;
    public MovePlayer    playerMove;
    public GunScript     playerGun;
    public BoxCollider2D playerColl;
    public PuppetManip   playerManip;
    public SpriteRenderer headRend;

    public Sprite headFront;
    public Sprite headBack;
    public GameObject player;
    public GameObject head;

	  public Text scoreText;
	  public Text timeText;
	  public Text timerText;

	  public int staticScoreMultiplier;
	  float dynamicScoreMultiplier = 1.0F;
	  int enemiesKilled;
	  int score;
	  float timerFloat;
	  int timerInt;

            // ----------- TEST Functionality - NOT IN USE YET ---------------- jonas point of no return
    // public int numOfPlayers = 1;                                               // Holds the number of active players at any given moment
    GameObject[] playerTags;
    List <GameObject> players = new List<GameObject>();         // An array with pointers to all the active players. Gets filled by PlayerSetup.cs
    GameObject selectedPlayer;                                        // Selected player at any given moment


    void Start () {                                                 // Use this for initialization


            if( GameObject.FindGameObjectsWithTag("Player").Length < 1 ) { Debug.Log("NO PLAYERS FOUND IN SCENE!"); }  // Important check

            foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("Player")) {    // Finds all tagged 'player' game objects in given scene
                players.Add(playerObject);
            };

            player = players[0];                                    // Player object is here for backwards with legacy scripts compability

// ---------- KEEP THIS UNTIL ENTIRE CODE IS REBASED TO NEW PLAYER SYSTEM -----------------------------
            head = GameObject.FindWithTag("Head");                  // Finds the head.
            playerTrans = player.GetComponent<Transform>();         // Defines the player components:
            playerAnim = player.GetComponent<Animator>();
            playerRigi = player.GetComponent<Rigidbody2D>();
            playerMove = player.GetComponent<MovePlayer>();
            playerGun = player.GetComponentInChildren<GunScript>();
            playerColl = player.GetComponent<BoxCollider2D>();
            playerManip = player.GetComponent<PuppetManip>();
            headRend = head.GetComponent<SpriteRenderer>();
// ---------- KEEP THIS UNTIL ENTIRE CODE IS REBASED TO NEW PLAYER SYSTEM -----------------------------

            scoreText = GameObject.Find("Score").GetComponent<Text>();
            timerText = GameObject.Find("Timer").GetComponent<Text>();
            timeText = GameObject.Find("Time:").GetComponent<Text>();

        Time.timeScale = 1f;                           // Sets time scale to 1 incrase sugarkick was active.
    }

                                                      // Update is called once per frame
    void Update () {
        if (timerText && timeText)                    // Updates the timer
        {
            timerFloat += Time.deltaTime;
            timerInt = (int)timerFloat;
            timerText.text = timerInt.ToString();
        }
    }

      // GENERIC PLAYERS INTERFACE
      // Description: Overriding the Player-function to support returning:
      //                1. Player GameObject
      //                2. Player child GameObjects
      //                3. Player Components
      //                4. Player child components
    public GameObject Player(int playerIndex){
        return players[playerIndex];
    }

    public GameObject Player(int playerIndex, string child) {
      return players[playerIndex].transform.Find(child).gameObject;
    }

    public T Player<T>(int playerIndex) where T : Component {
        return players[playerIndex].GetComponent<T>();
    }

    public T Player<T>(int playerIndex, string child) where T : Component {
      return players[playerIndex].transform.Find(child).GetComponent<T>();
    }

                                                        // Temporary scoring system
    public void NewScore(){
  		enemiesKilled++;
  		dynamicScoreMultiplier = (1.0F + (enemiesKilled / 100.0F));
  		score += (int)(dynamicScoreMultiplier*staticScoreMultiplier);
  		if(scoreText) scoreText.text = score.ToString();
  	}


    // --------------- UTILITY FUNCTIONS ------------------

                                        // Takes a Vector2 which has a variable absolute length
                                        // between 1 at 90 degrees, and sqrt(2) at 45 degrees
                                        // Returns a Vector2 which has an absolute length of 1
                                        // for all angles.
    public Vector2 DiagonalCompensate( Vector2 inVec ) {
                                        // Gets angle and converts it to radians
        float angle = GetAngle( Vector2.right, inVec) * Mathf.Deg2Rad;

        return new Vector2( inVec.x * Mathf.Abs(Mathf.Cos(angle)),
                            inVec.y * Mathf.Abs(Mathf.Sin(angle)) );
    }

                                        // Returns the correct angle between two vectors
    public float GetAngle( Vector2 v1, Vector2 v2 )
    {
        var sign = Mathf.Sign( v1.x * v2.y - v1.y * v2.x );
        return Vector2.Angle(v1, v2) * sign;
    }

                                        // Returns a unit vector given a specific angle
    public Vector2 GetUnitVector2(float angle) {

        return new Vector2( Mathf.Cos(angle), Mathf.Sin(angle) );
    }

                                        // Casts a Vector2 to a Vector3
    public Vector3 ToVector3( Vector2 vec2, float zval=0.0F ) {

        return new Vector3(vec2.x, vec2.y, zval);
    }
}


// TEST Functionality, DONT USE! (Jonas)
//void keepSelfAlive() {          // Makes sure that main scripts' parent are kept alive between scenes.
//  if ( GameObject.FindGameObjectsWithTag("MainCamera").Length > 1) {    // If there is more than 1 Main Camera, destroy this Camera
//    Debug.Log("There are two Main Cameras in the scene, destroying self....\n");
//    Destroy(gameObject);
//  } else {
//    DontDestroyOnLoad(gameObject);                   // Makes sure that the Main Camera is not Destroyed between scenes
//  }
//}
