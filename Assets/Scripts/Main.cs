using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {


    public int mapsize = 40;                    // Planning to have GLOBAL variables 
    public Transform playerTrans;               //  here. AY-AY Sir!
    public Animator playerAnim;
    public Rigidbody2D playerRigi;
    public movePlayer playerMove;

    GameObject player;
                                                // Use this for initialization
    void Start () {

        player = GameObject.Find("Player");
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

    // --------------- UTILITY FUNCTIONS ------------------

                                        // Takes a Vector2 which has a variable absolute length
                                        // between 1 at 90 degrees, and sqrt(2) at 45 degrees
                                        // Returns a Vector2 which has an absolute length of 1
                                        // for all angles.
    public Vector2 DiagonalCompensate( Vector2 inVec ) { 
                                        // Gets angle and converts it to radians
        float angle = GetAngle( Vector2.right, inVec) * Mathf.Deg2Rad;

        return new Vector2( inVec.x * Mathf.Abs(Mathf.Cos(angle)), 
                            inVec.y * Mathf.Abs(Mathf.Sin(angle)) );
    }
                  
                                        // Returns the correct angle between two vectors
    public float GetAngle( Vector2 v1, Vector2 v2 )
    {
        var sign = Mathf.Sign( v1.x * v2.y - v1.y * v2.x );
        return Vector2.Angle(v1, v2) * sign;
    }

                                        // Returns a unit vector given a specific angle
    public Vector2 GetUnitVector2(float angle) {

        return new Vector2( Mathf.Cos(angle), Mathf.Sin(angle) );
    }
              
                                        // Casts a Vector2 to a Vector3
    public Vector3 ToVector3( Vector2 vec2 ) {

        return new Vector3(vec2.x, vec2.y, 0.0f);
    }
}








