using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static int mapsize;

	// Use this for initialization
	void Start () {

		mapsize = 40;
	}
	
	// Update is called once per frame
	void Update () {


	}
		
	void restart() {

		Debug.Log ("Restarting game");
	}

	void save() {
		Debug.Log ("Saving game...");
	}

	void load(string filename){
		Debug.Log ("Loading game...");
	}
}
