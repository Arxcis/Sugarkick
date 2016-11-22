using UnityEngine;
using System.Collections;

public class JumperHoleChecker : MonoBehaviour {


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            print("Holy fuckings SHit!! Ther's a hole here!");
            GetComponentInParent<MoveJumper>().abortThereBeHoles();
        }
    }
}
