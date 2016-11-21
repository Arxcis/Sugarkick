using UnityEngine;
using System.Collections;

public class MoveJumper : MonoBehaviour {

    public int jumperRuteCalcRate = 15;         //Number of frames between each vector update.
    public float personalSpaceBro = 2.5f;       //How close does it have to get?
    public float friction = 1.5F;               //How quickly it stops when it hits the ground.
    public float jumpDistance = 3f;             //How far it jumps each time.
    public float jumpSpeed = 0.2f;
    public float walkAngleDiv = 20f;            //Range of angles the jumper will randomly add to its movement vector. ZigZag jumps.
    public float blockedAngleDiv = 50;          //Angle form tpPlayerVector it deviates if there is an obsitcle.
    public int jumpCooldown = 100;              //Frames from the moment it lands til it can jump again.

    Vector3 vectorToPlayer;
    Vector3 jumpedFromPos;
    Transform jumperTrans;
    Rigidbody2D jumperRigi;
    Main main;

    bool midAir = false;
    int coldwn = 0;
    float distn = 0;

    int framesCounted = 0;

    void Start ()
    {
        main = GameObject.Find("Camera").GetComponent<Main>();      //gets all the global info
        jumperTrans = GetComponent<Transform>();
        jumperRigi = GetComponent<Rigidbody2D>();
    }
	

	void Update ()
    {

        if (framesCounted == jumperRuteCalcRate)         // Updates the enemy's rute every x frames
        {                                               // Calculates length between player and enemy and
                                                        // and moves the player by a factor of this distance.    
            vectorToPlayer = new Vector3(jumperTrans.position.x - main.playerTrans.position.x,
                                         jumperTrans.position.y - main.playerTrans.position.y);
            framesCounted = 0;
        }
        else framesCounted++;


        if (vectorToPlayer.magnitude > personalSpaceBro && coldwn == 0)
        {
            jumpedFromPos = jumperTrans.position;                           //Sets the position the jumperjumper from.
            jumperRigi.velocity = -vectorToPlayer.normalized * jumpSpeed;   //Starts th jump by giving it velocity.
            //JumpUpAnimation Start now!                                    //Plays JumpUp animaion.
            midAir = true;                                                  // the enemy is now mid air.
            coldwn = jumpCooldown;                                          //start the time no next jump cooldown.
        }


        distn = new Vector3(jumperTrans.position.x - jumpedFromPos.x,
                     jumperTrans.position.y - jumpedFromPos.y).magnitude;

        if (midAir && distn > jumpDistance)
        {
            midAir = true;
        }



        if (coldwn != 0 && midAir == false) coldwn--;                       //redice the cooldown of gounded and not 0.
    }
}
