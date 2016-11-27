using UnityEngine;
using System.Collections;


public class MovePlayer : MonoBehaviour {


        // Private
    bool  isWalking = false;
    Vector2 inputVector = new Vector2(0,0);


    void Start ()
    {}

    void FixedUpdate ()                           // Fixed update is independent on frame rate
    {

      inputVector.x = Input.GetAxisRaw("MoveAxisX");
      inputVector.y = Input.GetAxisRaw("MoveAxisY");
                                                      // Uses diagonalCompensate in Main script.
                                                      // Returns a Vector which has length 1 no matter which
                                                      // way player is facing.
      Main.Player<Rigidbody2D>(0).velocity += Main.DiagonalCompensate( inputVector ) * Main.Player<PuppetManip>(0).movementSpeed;

                                                      // Adds friction to the player
      if (Main.Player<Rigidbody2D>(0).velocity.magnitude > 0 && Main.Player<PuppetManip>(0).friction!= 0) {
          Main.Player<Rigidbody2D>(0).velocity = Main.Player<Rigidbody2D>(0).velocity * (1 / Main.Player<PuppetManip>(0).friction);
      }

      if (Main.Player<Rigidbody2D>(0).velocity.magnitude > 2)          // Updates the animation if the player is walking
      {   isWalking = true;   }
      else
      {   isWalking = false;  }
      Main.Player<Animator>(0).SetBool("isWalking", isWalking);

    }
}
