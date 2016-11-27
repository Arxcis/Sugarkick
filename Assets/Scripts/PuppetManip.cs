using UnityEngine;
using System.Collections;

public class PuppetManip : MonoBehaviour
{
    public bool     isEnemy = false;
    public bool     isSpawnerChild = false;
    public int      life = 3;                               // amount of respawns
    public int      hP   = 3;                               // amount of hits taken per respwan
    public int      invincibilityFrames = 200;
    public float    movementSpeed=1;
    public float    fallingSpeedMultiplier = 0.1F;          //how fast does the player move xy while falling.
    public Vector3  spawnLocation = new Vector3( 0, 0, 0 );
    public int      framesToPlaySugarAnim = 30;
    public float    sugarTimeSlow = 0.5f; //time scales to this * time
    public float    sugarSpeedChange = 1.5f; //movementSpeed scales to this * movementSpeed
    public GameObject[] guns;
    public float friction = 1.4f;

    public AudioClip respawnSound;      //only for player.
    public AudioClip spawnSound;        //for player and enemies.
    public AudioClip deathSound;        //player and enemies.
    public AudioClip hurtSound;         // player and enemies.
    public AudioClip fallSound;         // player and enemies.

    int currentHp; //= hp (in start)
    public int invFrm = 0;
    int frmPlayedSgrKck = 0;
    bool hasSugarkick = false;

    int index;                        // objects position in the global list

    void Start()
    {
        currentHp = hP;
    }

    void FixedUpdate()
    {
        if (invFrm != 0) invFrm--;                  //decreases the inv frames unless its 0.
        if (hasSugarkick) frmPlayedSgrKck++;        //counts up for sugarkick().
        if (frmPlayedSgrKck == framesToPlaySugarAnim) SugarKickOff(); // turns off sugarkick when reached stop.
    }

    public void respawn( )
    {
        if (isEnemy) Destroy(gameObject);                       //Destroys the enemy after death/fall animation is done.
        if (life <= 0)
            { gameObject.SetActive(false); }                     //Die animation and simialr, insert Game over()
        else
        {
            currentHp = hP;                                     // refills hp after respawn
            Main.Player<Animator>(0).Play("PlayerIdle");
            gameObject.transform.position = spawnLocation;
            Main.Player<MovePlayer>(0).enabled = true;                        // the player can move afer respawning.
            Main.Player<GunScript>(0, "Gun").enabled = true;
            Main.Player<BoxCollider2D>(0).enabled = true;
        }
    }

    public void damage( int d, string hitBy)                    //Take hp and check if killed.
    {
        currentHp -= d;
        if ( currentHp <= 0 ) {
            kill(hitBy);                                                //call kill function with whatever the player got hit by.
        }
        else if(!isEnemy && invFrm == 0)
        {
            invFrm = invincibilityFrames;
            SoundManager.instance.bamPow(hurtSound);                    // plays hurt sound.
            Main.Player<Animator>(0).Play("PlayerHurt");                         //play hurt animation.

        }
    }


    public void kill(string deathBy)
    {
        if (isEnemy)
        {
            GetComponent<Rigidbody2D>().velocity *= fallingSpeedMultiplier;         //the enemy slows down after falling off.
            if (gameObject.tag.Contains("Enemy2"))
            {
                GetComponent<MoveJumper>().enabled = false;   //the enemy cant move mid air.
                GetComponentInChildren<CircleCollider2D>().enabled = false;
            }

            else GetComponent<MoveEnemy>().enabled = false;                              //the enemy cant move mid air.
            GetComponent<BoxCollider2D>().enabled = false;                          //the collider cant block bullets from beneeth the map.
            if (isSpawnerChild) GetComponentInParent<SpawnEnemies>().gotKilled(gameObject.tag); //tells the spawner that a child died. :'(
            if (deathBy == "fall")GetComponent<Animator>().Play("EnemyFallDown");      //starts the fall animation for the enemy.
            if (deathBy == "bullet")GetComponent<Animator>().Play("EnemyDeath");      //starts the death animation for the enemy.
            if (deathBy == "attack") GetComponent<Animator>().Play("EnemyDeath");       //WIll add an attack animation later.
        }
        else
        {
            life--;
            Main.Player<Rigidbody2D>(0).velocity *= fallingSpeedMultiplier;
            Main.Player<MovePlayer>(0).enabled = false;                    //player cannot move while fallling.
            Main.Player<GunScript>(0, "Gun").enabled = false;                     //Player cant shoot while falling off.
            Main.Player<BoxCollider2D>(0).enabled = false;                    //Player cant collide after falling the first time.

            if (deathBy == "fall")
            {
                SoundManager.instance.bamPow(fallSound);
                Main.Player<Animator>(0).Play("PlayerFallDown"); //Main.playerAnim.SetTrigger("TriggerFellDown");      //Animation runs respawn()
            }

            if (deathBy == "enemy")
            {
                SoundManager.instance.bamPow(deathSound);
                respawn();//insert other death animation instead
            }
        }
    }

   public void SugarKickOn ()
  {
            if (frmPlayedSgrKck != framesToPlaySugarAnim)
            {
                hasSugarkick = true;
                movementSpeed *= sugarSpeedChange;
                Time.timeScale *= sugarTimeSlow;
                Time.fixedDeltaTime *= sugarTimeSlow;
                Main.Player<Animator>(0).Play("Sugarkick");          //start the sugarkick animaton. This one runns the loop. calls Tick().

        }
    }
    public void SugarKickOff()
    {

                frmPlayedSgrKck = 0;              //restes the counter, ready for next sugarkick.
                hasSugarkick = false;
                movementSpeed /= sugarSpeedChange;
                Time.timeScale /= sugarTimeSlow;
                Time.fixedDeltaTime /= sugarTimeSlow;
                Main.Player<Animator>(0).Play("PlayerIdle"); //Goes back to normal idle animation.
    }


    void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.gameObject.CompareTag( "Hole" )  && !gameObject.tag.Contains("Enemy2")) {
            kill("fall");
        }
    	else if (other.gameObject.CompareTag("Bullet") && isEnemy){
		      if(!other.GetComponent<ProjectileInfo>().truePiercing && other.GetComponent<ProjectileInfo>().pierceNumber < 1) Destroy (other.gameObject);
		          other.GetComponent<ProjectileInfo> ().pierceNumber--;
		          damage(other.GetComponent<ProjectileInfo>().damage, "bullet");
    	}

    }

    public void setIndex(int i)
    {
      index = i;
    }

    public int getIndex()
    {
      return index;
    }

}
