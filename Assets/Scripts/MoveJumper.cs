using UnityEngine;
using System.Collections;

public class MoveJumper : MonoBehaviour {

    public int jumperRuteCalcRate = 15;            //Number of frames between each vector update.
    public float personalSpaceBro = 2.5f;
    public float friction = 1.5F;
    Vector3 vectorToPlayer;
    Transform jumperTrans;
    Main main;
    bool midAir = false;

    int framesCounted = 0;

    // Use this for initialization
    void Start ()
    {
        main = GameObject.Find("Camera").GetComponent<Main>();      //gets all the global info
        jumperTrans = GetComponent<Transform>();
    }
	
	// Update is called once per frame
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




    }
}
