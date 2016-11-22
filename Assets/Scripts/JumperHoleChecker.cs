using UnityEngine;
using System.Collections;

public class JumperHoleChecker : MonoBehaviour {

    public GameObject jumper;


    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = jumper.GetComponent<Rigidbody2D>().velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            print("Holy fuckings SHit!! Ther's a hole here!");
            jumper.GetComponent<MoveJumper>().abortThereBeHoles();
        }
    }
}
