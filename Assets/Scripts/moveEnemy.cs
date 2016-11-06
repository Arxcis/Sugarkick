using UnityEngine;
using System.Collections;

public class moveEnemy : MonoBehaviour {

    public float moveSpeed = 0.1f;                                  //Movement speed modifier.
    Transform tfEnemy;
    Animator aniEnemy;
    public Transform tfPlayer;                                      //Targets the players tansform.
    Vector3 vectorToPlayer;

    // Use this for initialization
    void Start ()
    {
        tfEnemy = GetComponent<Transform>();                        //Enemy's transform component.
        aniEnemy = GetComponent<Animator>();                        //Sprite animator .
	}
	
	// Update is called once per frame
	void Update ()
    {     
        //need to put in a line that calculates the vector to the player in a 0-1 perimeter and multiply it by movespeed
//        vectorToPlayer = new Vector3((tfPlayer.position.x - tfEnemy.position.x), (tfPlayer.position.y - tfEnemy.position.y));
        

        tfEnemy.position += new Vector3(moveSpeed, 0);              //Moves enemy moveSpeed in x axis.
        aniEnemy.SetFloat("Speed", Mathf.Abs(moveSpeed));           //Updates animator. So it knows when its moving.
	}
}
