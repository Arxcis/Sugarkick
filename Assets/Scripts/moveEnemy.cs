using UnityEngine;
using System.Collections;

public class moveEnemy : MonoBehaviour {

    public float moveSpeed = 0.1f;                                  //Movement speed modifier.
    public int enemyRuteCalcRate = 15;                              //Number of frames between each vector update.
    public float personalSpaceBro = 2.5f;
    bool isWalking = false;                                         //used to trigger animation
    int framesCounted = 0;                                          //counts frames since last vector update.
    Transform tfEnemy;
    Animator aniEnemy;
    public Transform tfPlayer;                                      //Targets the players tansform.
    Vector3 vectorToPlayer;


                                                                     // Use this for initialization
    void Start ()
    {
        tfEnemy = GetComponent<Transform>();                        //Enemy's transform component.
        aniEnemy = GetComponent<Animator>();                        //Sprite animator .
	}
	
	                                                                // Update is called once per frame
	void FixedUpdate ()
    {
        
        if (framesCounted == enemyRuteCalcRate)                     //updates the enemy's rute every x frames
        {												    	    // Calculates length between player and enemy and
        												            // and moves the player by a factor of this distance.
            vectorToPlayer = new Vector3(tfEnemy.position.x - tfPlayer.position.x,
                                         tfEnemy.position.y - tfPlayer.position.y);
            framesCounted = 0;
        }
        else framesCounted++;

                                                                    //Moves enemy moveSpeed in x axis.
        if (vectorToPlayer.magnitude > personalSpaceBro)
        {
            tfEnemy.position += -vectorToPlayer.normalized * moveSpeed;
            isWalking = true;
        }
        else                                                        //enemy has reached the player
        {
            isWalking = false;
            print("Player Hit!");
        }

        aniEnemy.SetBool("isWalking", isWalking);                   //Updates animator. So it knows when its moving.
	}
}
