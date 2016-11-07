using UnityEngine;
using System.Collections;

public class moveEnemy : MonoBehaviour {

    public float moveSpeed = 0.1f;                                  //Movement speed modifier.
    public int enemyRuteCalcRate = 20;                              //Number of frames between each vector update.
    int framesCounted = 0;                                          //counts frames since last vector update.
    Transform tfEnemy;
    Animator aniEnemy;
    public Transform tfPlayer;                                      //Targets the players tansform.
    Vector3 vectorToPlayer;


    // Use this for initialization
    void Start ()
    {
        tfEnemy = GetComponent<Transform>();                        //Enemy's transform component.
        aniEnemy = GetComponent<Animator>();                        //Sprite animator .
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (framesCounted == enemyRuteCalcRate)
        {
            vectorToPlayer = new Vector3(Mathf.Sin(tfPlayer.position.x - tfEnemy.position.x) * moveSpeed,
                                         Mathf.Cos(tfPlayer.position.y - tfEnemy.position.y) * moveSpeed);
            framesCounted = 0;
        }
        else framesCounted++;

        tfEnemy.position += vectorToPlayer;              //Moves enemy moveSpeed in x axis.
        aniEnemy.SetFloat("Speed", Mathf.Abs(moveSpeed));           //Updates animator. So it knows when its moving.
	}
}
