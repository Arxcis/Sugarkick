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
        transPlayer.position += moveDirVector * playerSpeed;

        if (moveDirVector.x > 0 && moveDirVector.y > 0)
            isWalking = true;                      //updates the animation if the player is walking or...
        else isWalking = false;                     //if the player is not walking.

        animPlayer.SetBool("isPlayerWalking", isWalking);

        Debug.Log("yMove: " + moveDirVector.y + "  xMove: " + moveDirVector.x + '\n');

		if (rb.velocity.magnitude > 0) {
			rb.velocity = rb.velocity * (1 / friction);
		}
    }
}
