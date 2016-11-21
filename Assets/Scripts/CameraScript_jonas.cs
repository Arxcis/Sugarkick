using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript_jonas : MonoBehaviour {

    Main main;

    public GameObject[] players;

    public int sizeBuffer = 0; 
    public float camSizeBreakPoint = 10.0F;
    public float camExpandSpeed = 0.45F, camExpandAdjust = 5.5F;


    Camera      cam;      
    Transform   camTrans;
    Transform[] playersTrans;

    int        numPlayers;
    float      maxDistance;
    List<float> distances = new List<float>();

    float xSum, ySum;
    Vector2 centerOfMass = new Vector2(0, 0); // Formula @ http://hyperphysics.phy-astr.gsu.edu/hbase/cm.html

	void Start () {
        main = GameObject.Find("Camera").GetComponent<Main>();            
        camTrans = gameObject.GetComponent<Transform>();    // Camera transform
	    cam      = GetComponent<Camera>();       // To manipulate camsize

        playersTrans = new Transform[players.Length];
        int i=0;                                // Getting all players transforms
        foreach(GameObject player in players) {
            playersTrans[i] = player.GetComponent<Transform>();
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
            // Reset data each frame
        numPlayers = players.Length;             //  Number of players
        xSum = 0; ySum = 0; distances.Clear();

            // Center of mass calculation and max distance calculation
        foreach( Transform pt in playersTrans ) {
            xSum += pt.position.x;
            ySum += pt.position.y;
                                    // Compares every player to every other player
            foreach( Transform ptCompare in playersTrans ) {
                distances.Add((ptCompare.position - pt.position).magnitude);
            }
        }

        centerOfMass.x = xSum / numPlayers;
        centerOfMass.y = ySum / numPlayers;
        maxDistance = getMax( distances );
        Debug.Log("premaxd = " + maxDistance);
        maxDistance += sizeBuffer;
        maxDistance = (maxDistance < camSizeBreakPoint) ? camSizeBreakPoint : 
                              maxDistance * camExpandSpeed + camExpandAdjust;

        camTrans.position = main.ToVector3( centerOfMass, -1);  // Center of mass = camera position
        cam.orthographicSize =  maxDistance;                         // Camera size varies depending on how 
                                                                    // far the players away from each other.
        Debug.Log("COM = " + centerOfMass + '\n' + " maxd = " + maxDistance);
	}

    float getMax(List<float> floats){

        float max = 0;
        foreach(float f in floats) {
            if (f > max) 
                max = f;   
        }
        return max;
    }
}
