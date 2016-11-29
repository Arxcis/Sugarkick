using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{
        // Public:
    public float moveSpeed = 0.1f;                  //Movement speed modifier.
    public int   enemyRuteCalcRate = 15;            //Number of frames between each vector update.
	  public int   checkForNewTargetRate = 150;       // Frames between each new target-check.
	  public int   retargetCD = 400;
    public float personalSpaceBro = 2.5f;
    public float friction = 1.1F;

      // Private:
    Transform enemyTrans;
    Animator enemyAnim;
    Rigidbody2D enemyRigi;

    bool    isWalking     = false;                      //used to trigger animation
    int     framesCounted = 0;                          //counts frames since last vector update.
	  int     framesCounted2 = 0;
	  int     framesCounted3 = 0;
	  bool    justChangedTarget = false;
    Vector2 vectorToPlayer;

	  int targetIndex = 0;
                                                         // Use this for initialization
    void Start ()
    {
        enemyTrans = GetComponent<Transform>();          //Enemy's transform component.
        enemyAnim = GetComponent<Animator>();            //Sprite animator.
        enemyRigi = GetComponent<Rigidbody2D>();
    }

    // Fixed update is frame-rate independent
    void FixedUpdate ()
    {

		if (justChangedTarget == true)
		{
			framesCounted3++;
			if (framesCounted3 >= retargetCD)
			{
				justChangedTarget = false;
				framesCounted3 = 0;
			}
		}

		if (framesCounted2 >= checkForNewTargetRate && justChangedTarget == false)
		{	                                            // Checks for a new player to target every x frame.
			int k = targetIndex;                        // Calls function to find the closest player and
			UpdateTarget();                             // update the target to the closest one.

			if (k != targetIndex)
			{
				justChangedTarget = true;               // If the current target switches, we want a delay
			}                                           // before the next switch.

			framesCounted = 0;
		}
		else framesCounted2++;



        if (framesCounted >= enemyRuteCalcRate)         // Updates the enemy's rute every x frames
        {                                               // Calculates length between player and enemy and
                                                        // and moves the player by a factor of this distance.
			ChangeTarget(targetIndex);
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


            if (Main.Player<PuppetManip>( targetIndex ).invFrm == 0)           //if the player is not invincible, hit player!
            {
                Main.Player<PuppetManip>( targetIndex ).damage(1, "enemy");
                GetComponent<PuppetManip>().kill("attack");
            }

        }

        enemyAnim.SetBool("isWalking", isWalking);       //Updates animator. So it knows when its moving.
    }

	void UpdateTarget()                                  // Sets the current target to the closest player.
	{
		for (int i = 0; i < Main.Players().Count; i++)
		{
	// If distance between the nemy and this player is less than the distance from the current target, switch to that target.
			if(new Vector2(enemyTrans.position.x - Main.Player<Transform>(i).position.x,
				           enemyTrans.position.y - Main.Player<Transform>(i).position.y).magnitude <= vectorToPlayer.magnitude)
			{
				targetIndex = i;
				ChangeTarget (targetIndex);
			}
		}

		ChangeTarget (targetIndex);
	}


	void ChangeTarget(int pIndex)                      // Sets the vectortoPlayer to be the current targets current position.
	{
		vectorToPlayer = new Vector2 (enemyTrans.position.x - Main.Player<Transform> (pIndex).position.x,
			                          enemyTrans.position.y - Main.Player<Transform> (pIndex).position.y);

	}
}
