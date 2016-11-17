using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float spawnrate = 1f;   //Number of enemies spawned per second  
	public GameObject[ ] enemise;//Container for enemies that this spawner can spawn 
	public float spawnRange = 1f;

	private float spawnProgress=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int hello = enemise.Length - 1;
		spawnProgress += Time.deltaTime * spawnrate;
		float spawnLocation = Random.Range( 0, spawnRange ); 
		if ( spawnProgress >= 1 ) {
			
			for ( int i  = 0; i  < Mathf.Floor(spawnProgress); i ++ ) {
				Instantiate( enemise[ ( int )( Mathf.Floor( Random.Range( 0, enemise.Length - 1 ) ) ) ] );
			}
			spawnProgress--;
		}
	} 
}
