using UnityEngine;
using System.Collections;

public class puppetManip : MonoBehaviour {

	public bool isEnemy = false;
    public int   life = 1;                               // amount of respawns
    public int   hP   = 3;                               // amount of hits taken per respwan
    public float movementSpeed=1;
    public Vector3 spawnLocation = new Vector3( 0, 0, 0 );

	public spawnEnemies spawnE;

	void Start(){
		if (isEnemy)
			spawnE = GetComponentInParent<spawnEnemies>();
	}

    void respawn( ) {
        gameObject.transform.position = spawnLocation;
    }

    void damage( int d ) {                               //Take hp and check if killed.
        hP -= d;
        if ( hP <= 0 ) {
            kill( );
        } 
    } 

    void kill( ) {
        life--;

	if (isEnemy) {
		spawnE.spawnEnemy ();
		Destroy (gameObject);
	}

        if ( life <= 0) {
            gameObject.SetActive( false );
                                                         /* Die animation and simialr */
        }
        else {
            respawn( );
        }
    }

    void OnTriggerEnter2D( Collider2D other ) {
        if ( other.gameObject.CompareTag( "Hole" ) ) {
            kill( );
        }
	else if(other.gameObject.CompareTag("Bullet") && isEnemy){
		//damage (other.transform.GetComponent<Damage> ().damage);
		Destroy (other.gameObject);
		damage(1);
	}
    }
}