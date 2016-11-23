using UnityEngine;
using System.Collections;

public class PuppetManip : MonoBehaviour {


    Main main;

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

    int currentHp; //= hp (in start)
    public int invFrm = 0;
    int frmPlayedSgrKck = 0;
    bool hasSugarkick = false;

    void Start()
    {
        main = GameObject.Find("Camera").GetComponent<Main>();
        currentHp = hP;
    }

    void FixedUpdate()
    {
        if (invFrm != 0) invFrm--;                  //decreases the inv frames unless its 0.
        if (hasSugarkick) frmPlayedSgrKck++;        //counts up for sugarkick().
        if (frmPlayedSgrKck == framesToPlaySugarAnim) SugarKickOff(); // turns off sugarkick when reached stop.
    }

    public void respawn( ) {

        if (isEnemy) Destroy(gameObject);                       //Destroys the enemy after death/fall animation is done.
        if (life <= 0)
            { gameObject.SetActive(false); }                     //Die animation and simialr, insert Game over()
        else
        {
            currentHp = hP;                                     // refills hp after respawn
            main.playerAnim.Play("PlayerIdle");
            gameObject.transform.position = spawnLocation;
            main.playerMove.enabled = true;                        // the player can move afer respawning.
            main.playerGun.enabled = true;
            main.playerColl.enabled = true;
        }
    }

    public void damage( int d, string hitBy) {                               //Take hp and check if killed.
        currentHp -= d;
        if ( currentHp <= 0 ) {
            kill(hitBy);                                                //call kill function with whatever the player got hit by.
        }
        else if(!isEnemy && invFrm == 0)
        {
            invFrm = invincibilityFrames;
            main.playerAnim.Play("PlayerHurt");                         //play hurt animation.

        }
    }


    public void kill(string deathBy) {

        if (isEnemy)
        {
            GetComponent<Rigidbody2D>().velocity *= fallingSpeedMultiplier;         //the enemy slows down after falling off.
            if (gameObject.CompareTag("Jumper"))
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
            main.playerRigi.velocity *= fallingSpeedMultiplier;
            main.playerMove.enabled = false;                    //player cannot move while fallling.
            main.playerGun.enabled = false;                     //Player cant shoot while falling off.
            main.playerColl.enabled = false;                    //Player cant collide after falling the first time.

            if (deathBy == "fall") main.playerAnim.Play("PlayerFallDown"); //main.playerAnim.SetTrigger("TriggerFellDown");      //Animation runs respawn()
            if (deathBy == "enemy") respawn();//insert other death animation instead
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
                main.playerAnim.Play("Sugarkick");          //start the sugarkick animaton. This one runns the loop. calls Tick().

        }
    }
    public void SugarKickOff()
    {

                frmPlayedSgrKck = 0;              //restes the counter, ready for next sugarkick.
                hasSugarkick = false;
                movementSpeed /= sugarSpeedChange;
                Time.timeScale /= sugarTimeSlow;
                Time.fixedDeltaTime /= sugarTimeSlow;
                main.playerAnim.Play("PlayerIdle"); //Goes back to normal idle animation.
    }


    void OnTriggerEnter2D( Collider2D other ) {
        if ( other.gameObject.CompareTag( "Hole" )  && !gameObject.CompareTag("Jumper")) {
            kill("fall");
        }
    	else if (other.gameObject.CompareTag("Bullet") && isEnemy){
		if(!other.GetComponent<ProjectileInfo>().truePiercing && other.GetComponent<ProjectileInfo>().pierceNumber < 1) Destroy (other.gameObject);
		other.GetComponent<ProjectileInfo> ().pierceNumber--;
		damage(other.GetComponent<ProjectileInfo>().damage, "bullet");
    	}
      else if (!isEnemy && other.gameObject.CompareTag("Pickup / gun")) {
      for (int i=0; i< guns.Length; ++i) {
          guns[i].SetActive(false);
        }
        switch(other.gameObject.name){
          case "GunDrop1":
            guns[1].SetActive(true);
          break;
          case "GunDrop2":
            guns[2].SetActive(true);
          break;
          case "GunDrop3":
            guns[3].SetActive(true);
          break;
        }
			Destroy (other.gameObject);
        //GameObject.Find(other.gameObject.name).SetActive(true);
        //GameObject.GetChild("Gun").gameObject.setActive(false);
      }
    }


}
