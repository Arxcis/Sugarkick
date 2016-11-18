using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	public bool endlessMode = false;
	spawnEnemies spwnE;

	// Use this for initialization
	void Start () {
		spwnE = gameObject.GetComponentInParent<spawnEnemies> ();
	}

	void  OnDestroy(){
		if(endlessMode) spwnE.spawnEnemy ();
	}
}
