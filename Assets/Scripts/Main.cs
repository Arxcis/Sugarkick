using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {


	public int mapsize = 40;                    // Planning to have GLOBAL variables 
    public Transform playerTrans;   			//  here. AY-AY Sir!
    public Animator playerAnim;
    public Rigidbody2D playerRigi;
	                                            // Use this for initialization
	void Start () {
        playerTrans = GameObject.Find("TestPlayer").GetComponent<Transform>();
        playerAnim  = GameObject.Find("TestPlayer").GetComponent<Animator>();
        playerRigi  = GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>();
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
