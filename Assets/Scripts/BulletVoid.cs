using UnityEngine;
using System.Collections;

public class BulletVoid : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D( Collider2D other ) {
		if(other.gameObject.CompareTag("Bullet")){
			Destroy (other.gameObject);
		}
	}
}
