using UnityEngine;
using System.Collections;
using System.Collections.Generic;          // List<type>()
using UnityEngine.UI;

              // Declaring a static class, the only thing you are telling the compiler is that
              // all the members of the class have to be static.
              // A static class cannot be instantiated!
public class Main : MonoBehaviour {

        // Public
    public bool inMainMenu = false;
    public static bool mouseOn    = true;          // mouse on / off

    public static int numberOfPlayers = 4;	// '= 4' is temporary, for testing.
    public GameObject playerPrefab;

    public Sprite headFront;
    public Sprite headBack;

	  public static Text scoreText;
	  public static Text timeText;
	  public static Text timerText;

	  public static int staticScoreMultiplier;

        // Private
	  static float dynamicScoreMultiplier = 1.0F;
	  static int enemiesKilled;
	  static int score;
	  static float timerFloat;
	  static int timerInt;

              // NEW PLAYER SYSTEM
    static List<GameObject> players = new List<GameObject>();    // An array with pointers to all the active players.
    static GameObject selectedPlayer;                            // Selected player at any given moment

    void Awake ()
    {
      DontDestroyOnLoad (gameObject);				// Makes sure the camera persists through scenes.

      if(inMainMenu == false)					// Don't want to do this in MainMenu
    	{
    	    SpawnPlayers();						// Removes the current players before adding the correct amount.

          if( GameObject.FindGameObjectsWithTag("Player").Length < 1 ) { Debug.Log("NO PLAYERS FOUND IN SCENE!"); }  // Important check

    	    if (GameObject.FindGameObjectsWithTag ("MainCamera").Length > 1) // If there already exists a camera in the scene(loaded from previous scene).
    	    {
    	        Destroy (gameObject); 					// Destroys this camera as to have no duplicates.
    	    }

          scoreText = GameObject.Find("Score").GetComponent<Text>();
          timerText = GameObject.Find("Timer").GetComponent<Text>();
          timeText = GameObject.Find("Time:").GetComponent<Text>();

          Time.timeScale = 1f;                           // Sets time scale to 1 incrase sugarkick was active.

    	}
    	inMainMenu = false;
    }

                                                      // Update is called once per frame
    void Update () {
        if (timerText && timeText)                    // Updates the timer
        {
            timerFloat += Time.deltaTime;
            timerInt = (int)timerFloat;
            timerText.text = timerInt.ToString();
        }
    }





    // ************************** INSTANTIATION OF PLAYERS ***********************
    void SpawnPlayers()					// Removes the current players before adding the correct amount.
    {
        int k = 0;
	      foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("Player"))
	      {
            Debug.Log("Destroy player" + k);
	          Destroy  ( playerObject );
            k++;
	      }

        players.Clear();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            Debug.Log("Create player" + i);
            players.Add( Instantiate (playerPrefab, new Vector2(0.0F, 0.0F), Quaternion.identity) as GameObject );
            players[i].GetComponent<PuppetManip>().SetIndex(i);
        };
    //var enemy = Instantiate (enemiesToSpawn[enemyType], trans.position, Quaternion.identity) as GameObject;
    }











    /* GENERAL PLAYER INTERFACE FUNCTIONS
     * Description: Overriding the Player-function to support returning multiple types:
     *                0. Players()                   -> List<GameObject>
     *                1. Player(index)               -> GameObject
     *                2. Player(index, childname)    -> GameObject
     *                3. Player<component>(index)    -> Component
     *                4. Player<component>(index, childname) -> Component
    */
    public static List <GameObject> Players()
    {
        return players;
    }

    public static GameObject Player(int playerIndex)
    {
        return players[playerIndex];
    }

    public static GameObject Player(int playerIndex, string child)
    {

      return players[playerIndex]
               .transform
                 .Find(child)
                   .gameObject;
    }

    public static T Player<T>(int playerIndex) where T : Component
    {
        return players[playerIndex]
                 .GetComponent<T>();
    }

    public static T Player<T>(int playerIndex, string child) where T : Component
    {
      return players[playerIndex]
                 .transform
                   .Find(child)
                     .gameObject
                       .GetComponent<T>();

    }




    public static void toggleMouseAiming()         //useed by toggle ui element in pause menu.
    {
        print("Switched mouse-aiming from: " + mouseOn + " to: " + !mouseOn);
        if (mouseOn) mouseOn = false;
        else mouseOn = true;
    }


                                            // Temporary scoring system
    public static void NewScore()
    {
  		enemiesKilled++;
  		dynamicScoreMultiplier = (1.0F + (enemiesKilled / 100.0F));
  		score += (int)(dynamicScoreMultiplier*staticScoreMultiplier);
  		if(scoreText) scoreText.text = score.ToString();
  	}


    // --------------- UTILITY FUNCTIONS ------------------

                                        // Takes a Vector2 which has a variable absolute length
                                        // between 1 at 90 degrees, and sqrt(2) at 45 degrees
                                        // Returns a Vector2 which has an absolute length of 1
                                        // for all angles.
    public static Vector2 DiagonalCompensate( Vector2 inVec )
    {                                    // Gets angle and converts it to radians
        float angle = GetAngle( Vector2.right, inVec) * Mathf.Deg2Rad;

        return new Vector2( inVec.x * Mathf.Abs(Mathf.Cos(angle)),
                            inVec.y * Mathf.Abs(Mathf.Sin(angle)) );
    }

                                        // Returns the correct angle between two vectors
    public static float GetAngle( Vector2 v1, Vector2 v2 )
    {
        v1 = v1.normalized;    v2 = v2.normalized;
        float sign = Mathf.Sign( v1.x * v2.y - v1.y * v2.x );
        return Vector2.Angle(v1, v2) * sign;
    }

                                        // Returns a unit vector given a specific angle
    public static Vector2 GetUnitVector2(float angle)
    {
        return new Vector2( Mathf.Cos(angle), Mathf.Sin(angle) );
    }

                                        // Casts a Vector2 to a Vector3
    public static Vector3 ToVector3( Vector2 vec2, float zval=0.0F )
    {
        return new Vector3(vec2.x, vec2.y, zval);
    }

    public static float pitchDeviation (float range)       //used to give a deviation in pitch when playing a sound. normal value is 0.1f:
    {
        return 1f + Random.Range(-range, range);    //meaning that the pich can go from 0.90f t0 1.10f.
    }
}


// TEST Functionality, DONT USE! (Jonas)
//void keepSelfAlive() {          // Makes sure that main scripts' parent are kept alive between scenes.
//  if ( GameObject.FindGameObjectsWithTag("MainCamera").Length > 1) {    // If there is more than 1 Main Camera, destroy this Camera
//    Debug.Log("There are two Main Cameras in the scene, destroying self....\n");
//    Destroy(gameObject);
//  } else {
//    DontDestroyOnLoad(gameObject);                   // Makes sure that the Main Camera is not Destroyed between scenes
//  }
//}
