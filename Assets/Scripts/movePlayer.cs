using UnityEngine;
using System.Collections;


public class movePlayer : MonoBehaviour {
   	
    Main main;  

		// Public:
	public Vector2 playerSpeed = new Vector2(10,10);   	  // Get speed from puppetManip.cs at some point
	public float friction      = 1.1F;   

		// Private:
    Animator   animPlayer;
   	Rigidbody2D        rb;
   										
    bool      isWalking = false;
    Vector3 inputVector = new Vector2(0, 0);

						    				
	void Start () {
		main = GameObject.Find("Camera").GetComponent<Main>();  // Getting main script

        animPlayer  = GetComponentInChildren<Animator>();
		rb          = GetComponent<Rigidbody2D> ();
	}

						            // Fixed update is independent on frame rate
	void FixedUpdate () {
									// Comment to next 10 lines:
        inputVector.x = Input.GetAxisRaw("MoveAxisX");
        inputVector.y = Input.GetAxisRaw("MoveAxisY");

		rb.velocity = Vector2.Scale( main.diagonalCompensate(inputVector), playerSpeed );

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