using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour {
	//hello Testing

	public static int speed = 6;

	Transform transPlayer;
	Vector3 UP    = new Vector3(  0, speed );    // Since unity works in 3 dimensions we have to
	Vector3 DOWN  = new Vector3(  0,-speed );    // create a Vector3 x,y,z, even though we do not
	Vector3 RIGHT = new Vector3(  speed, 0 );    // care about the z.
	Vector3 LEFT  = new Vector3( -speed, 0 );

	// Use this for initialization
	void Start () {
		transPlayer = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("up")) {
			print ("up");
			transPlayer.position += UP * Time.deltaTime;
		}

		if (Input.GetKey ("down")) {
			print ("down");
			transPlayer.position += DOWN * Time.deltaTime;
		}

		if (Input.GetKey ("left")) {
			print ("left");
			transPlayer.position += LEFT * Time.deltaTime;
		}
		if (Input.GetKey ("right")) {
			print ("right");
			transPlayer.position += RIGHT * Time.deltaTime;
		}

	}
}
