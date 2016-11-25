using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

        // Public:
    public float moveSpeed = 0.1f;                  //Movement speed modifier.
    public int   enemyRuteCalcRate = 15;            //Number of frames between each vector update.
    public float personalSpaceBro = 2.5f;
    public float friction = 1.1F;
    Main main;                                      //gets global veiables form main


    // Private:
    Transform enemyTrans;
    Animator enemyAnim;
    Rigidbody2D enemyRigi;

    bool    isWalking     = false;                  //used to trigger animation
    int     framesCounted = 0;                      //counts frames since last vector update.
    Vector3 vectorToPlayer;
                                                        // Use this for initialization
    void Start ()
    {
        enemyTrans = GetComponent<Transform>();            //Enemy's transform component.
        enemyAnim = GetComponent<Animator>();            //Sprite animator.
        enemyRigi = GetComponent<Rigidbody2D>();
        main = GameObject.Find("Camera").GetComponent<Main>();
    }

    // Fixed update is frame-rate independent
    void FixedUpdate ()
    {
        if (framesCounted == enemyRuteCalcRate)         // Updates the enemy's rute every x frames
        {                                               // Calculates length between player and enemy and
                                                        // and moves the player by a factor of this distance.
            vectorToPlayer = new Vector3(enemyTrans.position.x - main.Player<Transform>(0).position.x,
                                         enemyTrans.position.y - main.Player<Transform>(0).position.y);
            framesCounted = 0;
        }
        else framesCounted++;

                                                             //Moves enemy moveSpeed in x axis.
        if (vectorToPlayer.magnitude > personalSpaceBro)
        {
            enemyRigi.velocity = -vectorToPlayer.normalized * moveSpeed;
            isWalking = true;
        }
        else if (vectorToPlayer.magnitude > 0.1F)           //the magnitude is 0 when the enemy spawnes,
        {                                                   //Because it cant calculate vector to player if enemy trans us NULL.
            isWalking = false;
            enemyRigi.velocity *= (1 / friction);          // stops the enemy when inside personal space.


            if (main.Player<PuppetManip>(0).invFrm == 0)           //if the player is not invincible, hit player!
            {
                print("Player Hit!");
                main.Player<PuppetManip>(0).damage(1, "enemy");  
                GetComponent<PuppetManip>().kill("attack");
            }

        }

        enemyAnim.SetBool("isWalking", isWalking);       //Updates animator. So it knows when its moving.
    }
}
