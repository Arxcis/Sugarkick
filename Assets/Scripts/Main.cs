using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {


	public int mapsize = 40;                    // Planning to have GLOBAL variables 
    GameObject player;
    public Transform playerTrans;   			//  here. AY-AY Sir!
    public Animator playerAnim;
    public Rigidbody2D playerRigi;
    public movePlayer playerMove;
	                                            // Use this for initialization
	void Start () {
        player = GameObject.Find("TestPlayer");
        playerTrans = player.GetComponent<Transform>();
        playerAnim  = player.GetComponent<Animator>();
        playerRigi  = player.GetComponent<Rigidbody2D>();
        playerMove  = player.GetComponent<movePlayer>();

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
