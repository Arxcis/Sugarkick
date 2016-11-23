using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour {


    public int mapsize = 40;                    // Planning to have GLOBAL variables
    public bool isMainMenu = false;
    public float musicVol = 0.3f;
    public float effectsVol = 0.4f;
    public GameObject musicTrackbar;
    public GameObject effectsTrackbar;

    public Transform     playerTrans;           //  here. AY-AY Sir!
    public Animator      playerAnim;
    public Rigidbody2D   playerRigi;
    public MovePlayer    playerMove;
    public GunScript     playerGun;
    public BoxCollider2D playerColl;
    public PuppetManip   playerManip;
    public SpriteRenderer headRend;

    public Sprite headFront;
    public Sprite headBack;
    public GameObject player;
    public GameObject head;

	  public Text scoreText;
	  public Text timeText;
	  public Text timerText;

	  public int staticScoreMultiplier;
	  float dynamicScoreMultiplier = 1.0F;
	  int enemiesKilled;
	  int score;
	  float timerFloat;
	  int timerInt;

                                                // Use this for initialization
    void Start () {

        if (!isMainMenu)            //define player components in not in main menu. Player does not exist in menu.
        {
            player = GameObject.Find("Player");         //finds tha player game object in the scene.
            head = GameObject.Find("Head");             //finds the head.
            playerTrans = player.GetComponent<Transform>();     //Defines the player components:
            playerAnim = player.GetComponent<Animator>();
            playerRigi = player.GetComponent<Rigidbody2D>();
            playerMove = player.GetComponent<MovePlayer>();
            playerGun = player.GetComponentInChildren<GunScript>();
            playerColl = player.GetComponent<BoxCollider2D>();
            playerManip = player.GetComponent<PuppetManip>();

            headRend = head.GetComponent<SpriteRenderer>();
            scoreText = GameObject.Find("Score").GetComponent<Text>();
            timerText = GameObject.Find("Timer").GetComponent<Text>();
            timeText = GameObject.Find("Time:").GetComponent<Text>();
        }
        Time.timeScale = 1f;            //sets time scale to 1 incase sugarkick was active.
        Time.fixedDeltaTime = 1;         

    }

    // Update is called once per frame
    void Update () {
        if (!isMainMenu)
        {
            // Updates the timer
            if (timerText && timeText)
            {
                timerFloat += Time.deltaTime;
                timerInt = (int)timerFloat;
                timerText.text = timerInt.ToString();
            }
        }
    }

	void FixedUpdate(){

	}


    public void getOptionsvalues()
    {
        musicVol = musicTrackbar.GetComponent<Slider>().value;              //gets the value of the volume trackbar.
        effectsVol = effectsTrackbar.GetComponent<Slider>().value;          //gets the value of the effects trackbar.
        setSoundvalues();                                                   //calls the function that sets the values.
    }

    void setSoundvalues()
    {
        GetComponent<AudioSource>().volume = musicVol;                  //sets the volume values to the audiosources.
    }


    // Temporary scoring system
    public void NewScore(){
		enemiesKilled++;
		dynamicScoreMultiplier = (1.0F + (enemiesKilled / 100.0F));
		score += (int)(dynamicScoreMultiplier*staticScoreMultiplier);
		if(scoreText) scoreText.text = score.ToString();
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
    public Vector3 ToVector3( Vector2 vec2, float zval=0.0F ) {

        return new Vector3(vec2.x, vec2.y, zval);
    }
}
