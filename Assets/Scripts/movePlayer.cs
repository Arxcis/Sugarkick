using UnityEngine;
using System.Collections;


public class movePlayer : MonoBehaviour {
   
		// Public:
	public float playerSpeed = 1.0F;   	    	// Get speed from puppetManip.cs at some point
	public float friction    = 1.1F;   

		// Private:
    Animator   animPlayer;
   	Rigidbody2D        rb;
   										
    bool        isWalking = false;
    Vector3 moveDirVector = new Vector2(0, 0);

						    				
	void Start () {
        animPlayer  = GetComponentInChildren<Animator>();
		rb          = GetComponent<Rigidbody2D> ();
	}

						            // Fixed update is independent on frame rate
	void FixedUpdate () {
									// Comment to next 10 lines:
									// (jonas) This should be written in a more compact and readable 
									//  manner. Had to add 0.001F to the denominator, so it would not
									//  be zero, which is forbidden in a denominator. Returns {NaN}
        moveDirVector.x = Input.GetAxisRaw("MoveAxisX");
        moveDirVector.y = Input.GetAxisRaw("MoveAxisY");

									// Makes diagonal movement similar speed as vertical and horizontal
		moveDirVector.x = (moveDirVector.x * 1/(Mathf.Sqrt(moveDirVector.x * moveDirVector.x + 
														   moveDirVector.y * moveDirVector.y + 0.001F)));
		moveDirVector.y = (moveDirVector.y * 1/(Mathf.Sqrt(moveDirVector.x * moveDirVector.x + 
														   moveDirVector.y * moveDirVector.y + 0.001F)));
		rb.velocity = new Vector2 (rb.velocity.x + moveDirVector.x, rb.velocity.y + moveDirVector.y);


													// Adds friction to the player
		if (rb.velocity.magnitude > 0 && friction != 0) {
			rb.velocity = rb.velocity * (1 / friction);
                                         
        }

        if (rb.velocity.magnitude > 2)          // Updates the animation if the player is walking
        {   isWalking = true;   }               
        else									
        {   isWalking = false;  }
        animPlayer.SetBool("isPlayerWalking", isWalking);
    }
}