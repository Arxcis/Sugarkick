using UnityEngine;
using System.Collections;


public class MovePlayer : MonoBehaviour {
    
    Main main;  

        // Public:
    public float playerSpeed = 10.0F;    // Get speed from puppetManip.cs at some point
    public float friction    = 1.1F;   

        // Private:
    Animator   animPlayer;
    Rigidbody2D        rb;
                                        
    bool      isWalking = false;
    Vector2 inputVector = new Vector2(0, 0);

                                            
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
                                    // Uses diagonalCompensate in Main script.
                                    // Returns a Vector which has length 1 no matter which
                                    // way player is facing.
        rb.velocity = main.DiagonalCompensate( inputVector ) * playerSpeed;

                                                    // Adds friction to the player
        if (rb.velocity.magnitude > 0 && friction != 0) {
            rb.velocity = rb.velocity * (1 / friction);              
        }

        if (rb.velocity.magnitude > 2)          // Updates the animation if the player is walking
        {   isWalking = true;   }               
        else                                    
        {   isWalking = false;  }
        animPlayer.SetBool("isWalking", isWalking);
    }
}