using UnityEngine;
using System.Collections;


public class MovePlayer : MonoBehaviour {

    Main main;

        // Private
    bool  isWalking = false;
    Vector2 inputVector = new Vector2(0,0);


    void Start () {
        main = GameObject.Find("Camera").GetComponent<Main>();  // Getting main script
    }

    void FixedUpdate () {                             // Fixed update is independent on frame rate

      inputVector.x = Input.GetAxisRaw("MoveAxisX");
      inputVector.y = Input.GetAxisRaw("MoveAxisY");
                                                      // Uses diagonalCompensate in Main script.
                                                      // Returns a Vector which has length 1 no matter which
                                                      // way player is facing.
      main.Player<Rigidbody2D>(0).velocity += main.DiagonalCompensate( inputVector ) * main.Player<PuppetManip>(0).movementSpeed;

                                                      // Adds friction to the player
      if (main.Player<Rigidbody2D>(0).velocity.magnitude > 0 && main.Player<PuppetManip>(0).friction!= 0) {
          main.Player<Rigidbody2D>(0).velocity = main.Player<Rigidbody2D>(0).velocity * (1 / main.Player<PuppetManip>(0).friction);
      }

      if (main.Player<Rigidbody2D>(0).velocity.magnitude > 2)          // Updates the animation if the player is walking
      {   isWalking = true;   }
      else
      {   isWalking = false;  }
      main.Player<Animator>(0).SetBool("isWalking", isWalking);

    }
}
