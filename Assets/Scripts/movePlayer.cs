using UnityEngine;
using System.Collections;


public class movePlayer : MonoBehaviour {
   

public float playerSpeed;                                                //gets its value from the manip script

    Transform transPlayer;
    Animator animPlayer;
    bool isWalking = false;
    Vector3 moveDirVector = new Vector2(0, 0);
	Rigidbody2D rb;
	public float friction = 0;
	

	                                                                    // Use this for initialization
	void Start () {
		transPlayer = GetComponent<Transform>();
        animPlayer = GetComponentInChildren<Animator>();
		rb = gameObject.GetComponent<Rigidbody2D> ();
        playerSpeed = GetComponent<puppetManip>().movementSpeed;        //gets the movement speed from manip
	}

	                                                                    // Update is called once per frame
	void FixedUpdate () {

        moveDirVector.x = Input.GetAxisRaw("MoveAxisX") * playerSpeed;   //gets the axis for translation and multiplies it by playerspeed:
        moveDirVector.y = Input.GetAxisRaw("MoveAxisY") * playerSpeed; 
		                                                                 // Makes diagonal movement similar speed as vertical and horizontal.
		moveDirVector.x =  (moveDirVector.x * 1/(Mathf.Sqrt(moveDirVector.x * moveDirVector.x + moveDirVector.y * moveDirVector.y)));
		moveDirVector.y =  (moveDirVector.y * 1/(Mathf.Sqrt(moveDirVector.x * moveDirVector.x + moveDirVector.y * moveDirVector.y)));
		rb.velocity = new Vector2 (rb.velocity.x + moveDirVector.x, rb.velocity.y + moveDirVector.y);
                                                                         //transPlayer.position += moveDirVector * playerSpeed;


		                                                                // Adds friction to the player
		if (rb.velocity.magnitude > 0) {
			rb.velocity = rb.velocity * (1 / friction);
                                         
        }

        if (rb.velocity.magnitude > 2)                                //updates the animation if the player is walking or...
        {   isWalking = true;   }                                     //if the player is still. Needs to trigger a while before vel is 0.
        else
        { isWalking = false; }
        animPlayer.SetBool("isPlayerWalking", isWalking);
    }
}