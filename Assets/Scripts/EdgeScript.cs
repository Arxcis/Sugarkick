using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PolygonCollider2D))]    // Edge detection uses Polygoncollider2d trigger attribute
public class EdgeScript : MonoBehaviour {


    void Awake ()
    {
      gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;  // Enable Trigger attribute
    }

    void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player")){

            PuppetManip mnIp = other.GetComponent<PuppetManip>();     // Gets the exited object's puppit manip.
            mnIp.kill("fall");
        }
    }
}
