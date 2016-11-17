using UnityEngine;
using System.Collections;

public class getKeys : MonoBehaviour {				// Depricated script. May need deleting.
													// Its simplicity is cute thought 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("up")) {
			print ("Up!");
		}

		if (Input.GetKey ("down")) {
			print ("down");
		}

		if (Input.GetKey ("left")) {
			print ("left");
		
		}
		if (Input.GetKey ("right")) {
			print ("right");
		}
	}
}
