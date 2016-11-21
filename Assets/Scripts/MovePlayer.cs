using UnityEngine;
using System.Collections;


public class MovePlayer : MonoBehaviour {
    
    Main main;  

        // Private                       
    bool      isWalking = false;
    Vector2 inputVector = new Vector2(0, 0);

                                            
    void Start () {
        main = GameObject.Find("Camera").GetComponent<Main>();  // Getting main script
    }

                                    // Fixed update is independent on frame rate
    void FixedUpdate () {
                                    // Comment to next 10 lines:
        inputVector.x = Input.GetAxisRaw("MoveAxisX");
        inputVector.y = Input.GetAxisRaw("MoveAxisY");
                                    // Uses diagonalCompensate in Main script.
                                    // Returns a Vector which has length 1 no matter which
                                    // way player is facing.
        main.playerRigi.velocity += main.DiagonalCompensate( inputVector ) * main.playerManip.movementSpeed;

                                                    // Adds friction to the player
        if (main.playerRigi.velocity.magnitude > 0 && main.playerManip.friction != 0) {
            main.playerRigi.velocity *= (1 / main.playerManip.friction);              
        }

        if (main.playerRigi.velocity.magnitude > 2)          // Updates the animation if the player is walking
        {   isWalking = true;   }               
        else                                    
        {   isWalking = false;  }
        main.playerAnim.SetBool("isWalking", isWalking);


        
    }
}