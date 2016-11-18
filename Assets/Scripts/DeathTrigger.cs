using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	spawnEnemies spwnE;

	// Use this for initialization
	void Start () {
		spwnE = gameObject.GetComponentInParent<spawnEnemies> ();
	}

	void  OnDestroy(){
		spwnE.gotKilled (gameObject.tag);
	}
}
