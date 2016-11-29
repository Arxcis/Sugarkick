using UnityEngine;
using System.Collections;

public class EdgeScript : MonoBehaviour {
    void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player")){

            PuppetManip mnIp = other.GetComponent<PuppetManip>();   //gets the exited object's puppit manip.

            mnIp.kill("fall");
        }
    }
}
