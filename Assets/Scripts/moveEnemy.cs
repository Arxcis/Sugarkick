using UnityEngine;
using System.Collections;

public class moveEnemy : MonoBehaviour {
        
        // Public:
    public float moveSpeed = 0.1f;                  //Movement speed modifier.
    public int   enemyRuteCalcRate = 15;            //Number of frames between each vector update.
    public float personalSpaceBro = 2.5f;
    Main main;                                      //gets global veiables form main


    // Private:
    Transform enemyTrans;
    Animator enemyAnim;

    bool    isWalking     = false;                  //used to trigger animation
    int     framesCounted = 0;                      //counts frames since last vector update.
    Vector3 vectorToPlayer;
                                                        // Use this for initialization
    void Start ()
    {
        enemyTrans = GetComponent<Transform>();            //Enemy's transform component.
        enemyAnim = GetComponent<Animator>();            //Sprite animator.
        main = GameObject.Find("Camera").GetComponent<Main>();
    }

    // Fixed update is frame-rate independent
    void FixedUpdate ()
    {
        if (framesCounted == enemyRuteCalcRate)         // Updates the enemy's rute every x frames
        {                                               // Calculates length between player and enemy and
                                                        // and moves the player by a factor of this distance.    
            vectorToPlayer = new Vector3(enemyTrans.position.x - main.playerTrans.position.x,
                                         enemyTrans.position.y - main.playerTrans.position.y);
            framesCounted = 0;
        }
        else framesCounted++;

                                                        //Moves enemy moveSpeed in x axis.
        if (vectorToPlayer.magnitude > personalSpaceBro)
        {
            enemyTrans.position += -vectorToPlayer.normalized * moveSpeed;
            isWalking = true;
        }
        else                                            //enemy has reached the player
        {
            isWalking = false;
            print("Player Hit!");
        }

        enemyAnim.SetBool("isWalking", isWalking);       //Updates animator. So it knows when its moving.
    }
}
