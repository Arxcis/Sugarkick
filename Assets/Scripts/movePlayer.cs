using UnityEngine;
using System.Collections;


public class movePlayer : MonoBehaviour {
    //hello Testing
    
public float playerSpeed = 0.25f;

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
	}

	// Update is called once per frame
	void Update () {

        moveDirVector.x = Input.GetAxisRaw("MoveAxisX");
        moveDirVector.y = Input.GetAxisRaw("MoveAxisY");
		// Makes diagonal movement similar speed as vertical and horizontal.
		moveDirVector.x =  (moveDirVector.x * 1/(Mathf.Sqrt(moveDirVector.x * moveDirVector.x + moveDirVector.y * moveDirVector.y)));
		moveDirVector.y =  (moveDirVector.y * 1/(Mathf.Sqrt(moveDirVector.x * moveDirVector.x + moveDirVector.y * moveDirVector.y)));
		rb.velocity = new Vector2 (rb.velocity.x + moveDirVector.x, rb.velocity.y + moveDirVector.y);
        //transPlayer.position += moveDirVector * playerSpeed;

        if (moveDirVector.x > 0 && moveDirVector.y > 0)
            isWalking = true;                      //updates the animation if the player is walking or...
        else isWalking = false;                     //if the player is not walking.

        animPlayer.SetBool("isPlayerWalking", isWalking);

        //Debug.Log("yMove: " + moveDirVector.y + "  xMove: " + moveDirVector.x + '\n');
		print("Moving with a velocity of: " + rb.velocity.magnitude);

		// Adds friction to the player
		if (rb.velocity.magnitude > 0) {
			rb.velocity = rb.velocity * (1 / friction);
		}
    }
}