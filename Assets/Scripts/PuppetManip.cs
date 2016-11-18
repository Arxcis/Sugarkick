﻿using UnityEngine;
using System.Collections;

public class PuppetManip : MonoBehaviour {

	public bool isEnemy = false;
    public int   life = 1;                               // amount of respawns
    public int   hP   = 3;                               // amount of hits taken per respwan
    public float movementSpeed=1;
    public float fallingSpeedMultiplier = 0.1F;          //how fast does the player move xy while falling.
    public Vector3 spawnLocation = new Vector3( 0, 0, 0 );
    Main main;


    void Start()
    {
        // main = GameObject.Find("Camera").GetComponent<Main>();  // not used
    }
    
    public void respawn( ) {

        if (life <= 0)
            { gameObject.SetActive(false); }                     //Die animation and simialr, insert Game over()
        else
        {
            main.playerAnim.SetTrigger("TriggerRespawn");
            gameObject.transform.position = spawnLocation;
            main.playerMove.enabled = true;                        // the player can move afer respawning
            main.playerGun.enabled = true;
            main.playerColl.enabled = true;
        }
    }

    public void damage( int d ) {                               //Take hp and check if killed.
        hP -= d;
        if ( hP <= 0 ) {
            kill("enemy");
        } 
    } 


    public void kill(string deathBy) {

        if (isEnemy)
        {
            GetComponentInParent<SpawnEnemies>().gotKilled(gameObject.tag);
            Destroy(gameObject);
        }
        else
        {
            life--;
            main.playerRigi.velocity *= fallingSpeedMultiplier;
            main.playerMove.enabled = false;                    //player cannot move while fallling
            main.playerGun.enabled = false;
            main.playerColl.enabled = false;

            if (deathBy == "fall") main.playerAnim.Play("PlayerFallDown"); //main.playerAnim.SetTrigger("TriggerFellDown");      //Animation runs respawn()
            if (deathBy == "enemy") respawn();//insert other death animation instead
        }

    }

    void OnTriggerEnter2D( Collider2D other ) {
        if ( other.gameObject.CompareTag( "Hole" ) ) {
            kill("fall");
        }
    	else if (other.gameObject.CompareTag("Bullet") && isEnemy){
    		//damage (other.transform.GetComponent<Damage> ().damage);
			Destroy (other.gameObject);
    		damage(1);
    	}

    }
}