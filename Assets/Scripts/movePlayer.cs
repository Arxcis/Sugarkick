using UnityEngine;
using System.Collections;


public class movePlayer : MonoBehaviour {
    //hello Testing
    
public float playerSpeed = 0.25f;

    Transform transPlayer;
    Animator animPlayer;
    bool isWalking = false;
	Vector3 UP    = new Vector3(  0, 1 );    // Since unity works in 3 dimensions we have to
	Vector3 DOWN  = new Vector3(  0,-1 );    // create a Vector3 x,y,z, even though we do not
	Vector3 RIGHT = new Vector3(  1, 0 );    // care about the z.
	Vector3 LEFT  = new Vector3( -1, 0 );

	// Use this for initialization
	void Start () {
		transPlayer = GetComponent<Transform>();
        animPlayer = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () {

        
            if (Input.GetKey("up"))
            {
                print("up");
                transPlayer.position += UP * playerSpeed;
            }

            else if (Input.GetKey("down"))             //Cant go right while going left(else if)
            {
                print("down");
                transPlayer.position += DOWN * playerSpeed;
            }

            if (Input.GetKey("left"))
            {
                print("left");
                transPlayer.position += LEFT * playerSpeed;
            }
            else if (Input.GetKey("right"))             //Cant go right while going left(else if)
            {
                print("right");
                transPlayer.position += RIGHT * playerSpeed;
            }

        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
            isWalking = true;                      //updates the animation if the player is walking or...
        else isWalking = false;                     //if the player is not walking.

        animPlayer.SetBool("isPlayerWalking", isWalking);
    }
}
