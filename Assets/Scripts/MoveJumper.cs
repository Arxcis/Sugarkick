using UnityEngine;
using System.Collections;

public class MoveJumper : MonoBehaviour {

    public int jumperRuteCalcRate = 15;         //Number of frames between each vector update.
    public float personalSpaceBro = 2.5f;       //How close does it have to get?
    public float jumpDistance = 3f;             //How far it jumps each time.
    public float walkDiviation = 0.1f;          //Range of angles the jumper will randomly add to its movement vector. ZigZag jumps.
    public float noDeviationRange = 5f;         //Range of which the jumper will not add a deviation. To prevent it jumping away from the player.
    public float blockedAngleDiv = 50;          //Angle form tpPlayerVector it deviates if there is an obsitcle.
    public int jumpCooldown = 100;              //Frames from the moment it lands til it can jump again.

    Vector3 vectorToPlayer;
    Vector3 jumpedFromPos;
    Vector3 plannerCheckPos;
    Transform jumperTrans;
    Rigidbody2D jumperRigi;
    PuppetManip jumperPupp;
    Animator jumperAnim;
    BoxCollider2D jumpColl;
    // BoxCollider2D jumpPlanner;
    Main main;

    public bool midAir = false;
    int coldwn = 0;
    float distn = 0;
    float walkDiv = 0;

    int framesCounted = 0;

    void Start()
    {
        main = GameObject.Find("Camera").GetComponent<Main>();      //gets all the global info
        jumperTrans = GetComponent<Transform>();
        jumperRigi = GetComponent<Rigidbody2D>();
        jumperPupp = GetComponent<PuppetManip>();
        jumperAnim = GetComponent<Animator>();
        jumpColl = GetComponent<BoxCollider2D>();
        // jumpPlanner = GetComponentInChildren<BoxCollider2D>();

        gameObject.tag = "Jumper";                                  // sets a tag so Puppet knows what to do.
    }


    void FixedUpdate()
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

                jumperRigi.velocity = -vectorToPlayer;                //Starts th jump by giving it velocity.

                if (vectorToPlayer.magnitude > noDeviationRange)
                {
                    jumperRigi.velocity += new Vector2((Random.value + walkDiv) * Random.Range(-1, 1),
                                               Random.Range(-1, 1) * (Random.value + walkDiv)) * vectorToPlayer.magnitude;
                }
                jumperRigi.velocity = jumperRigi.velocity.normalized * jumperPupp.movementSpeed;


            midAir = true;                                                  // the enemy is now mid air.
            jumpColl.enabled = false;                                       //cant fall down a hole while midair.
            jumperAnim.Play("JumperTakeoff");                                  //Plays JumpUp animaion.
            jumperAnim.SetBool("midAir", midAir);
            coldwn = jumpCooldown;                                          //start the time no next jump cooldown.

        }


        distn = new Vector3(jumperTrans.position.x - jumpedFromPos.x,       //distance traveled since the jump started.
                     jumperTrans.position.y - jumpedFromPos.y).magnitude;

        if (midAir  && (distn >= jumpDistance || vectorToPlayer.magnitude < personalSpaceBro)) //when has the jumper traveled the disired distance?
        {
            jumperRigi.velocity *= (1 / jumperPupp.friction);                             //stop jumping. Land.
            midAir = false;                                                 //no longer in-air.
            jumpColl.enabled = true;
            jumperAnim.Play("JumperLand");
            jumperAnim.SetBool("midAir", midAir);
        }



        if (coldwn != 0 && midAir == false) coldwn--;                       //redice the cooldown of gounded and not 0.
    }

    public void abortThereBeHoles()
    {
        jumperRigi.velocity *= (1 / jumperPupp.friction);                             //stop jumping. Land.
        midAir = false;                                                 //no longer in-air.
        jumpColl.enabled = true;
        jumperAnim.Play("JumperLand");
        jumperAnim.SetBool("midAir", midAir);

    }

}
