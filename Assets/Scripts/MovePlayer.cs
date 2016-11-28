using UnityEngine;
using System.Collections;


public class MovePlayer : MonoBehaviour {


        // Private
    bool  isWalking = false;
    Vector2 testVector = Vector2.zero;

    void Start ()
    {}

    void FixedUpdate ()                           // Fixed update is independent on frame rate
    {
      testVector.x = Input.GetAxisRaw("MoveAxisX");
      testVector.y = Input.GetAxisRaw("MoveAxisY");
      Move( testVector );
    }

    public void Move(Vector2 inputVector)             // Uses diagonalCompensate in Main script.
    {                                                 // Returns a Vector which has length 1 no matter which
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
