using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float spawnrate = 1f;   //Number of enemies spawned per second  
	public GameObject[] enemies;//Container for enemies that this spawner can spawn 
	public float spawnRange = 1f;

	private float spawnProgress=0;

	
	// Update is called once per frame
	void Update () {

        spawnProgress += Time.deltaTime * spawnrate;
		if ( spawnProgress >= 1 ) {
			
			for ( int i  = 0; i  < Mathf.Floor(spawnProgress); i ++ ) {
				Instantiate( enemies[ ( int )( Mathf.Floor( Random.Range( 0, enemies.Length - 1 ) ) ) ] );
			}
			spawnProgress--;
		}
	} 
}
