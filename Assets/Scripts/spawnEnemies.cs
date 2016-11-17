using UnityEngine;
using System.Collections;

public class spawnEnemies : MonoBehaviour {

	public GameObject enemyToSpawn;
	public float spawnRange;

	public GameObject player1;
	public GameObject player2;

	public GameObject spawn1;
	public GameObject spawn2;
	public GameObject spawn3;
	public GameObject spawn4;
	public GameObject spawn5;
	public GameObject spawn6;

	Transform trans;

	// Use this for initialization
	void Start () {
		trans = spawn1.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnEnemy(){
		int i;
		bool tooClose;

		do {
		tooClose = false;
		i = Random.Range (1, 6);
			if(i == 1) {
				trans = spawn1.transform;
			if(Mathf.Sqrt((player1.transform.position.x - spawn1.transform.position.x)*(player1.transform.position.x - spawn1.transform.position.x) + (player1.transform.position.y - spawn1.transform.position.y)*(player1.transform.position.y - spawn1.transform.position.y)) >= spawnRange) 
					tooClose = true;
			}
				if(i == 2) {
				trans = spawn2.transform;
				if(Mathf.Sqrt((player1.transform.position.x - spawn2.transform.position.x)*(player1.transform.position.x - spawn2.transform.position.x) + (player1.transform.position.y - spawn2.transform.position.y)*(player1.transform.position.y - spawn2.transform.position.y)) >= spawnRange) 
					tooClose = true;
			}
			if(i == 3) {
				trans = spawn3.transform;
				if(Mathf.Sqrt((player1.transform.position.x - spawn3.transform.position.x)*(player1.transform.position.x - spawn3.transform.position.x) + (player1.transform.position.y - spawn3.transform.position.y)*(player1.transform.position.y - spawn3.transform.position.y)) >= spawnRange) 
					tooClose = true;
			}
			if(i == 4) {
				trans = spawn4.transform;
				if(Mathf.Sqrt((player1.transform.position.x - spawn4.transform.position.x)*(player1.transform.position.x - spawn4.transform.position.x) + (player1.transform.position.y - spawn4.transform.position.y)*(player1.transform.position.y - spawn4.transform.position.y)) >= spawnRange) 
					tooClose = true;
			}
			if(i == 5) {
				trans = spawn5.transform;
				if(Mathf.Sqrt((player1.transform.position.x - spawn5.transform.position.x)*(player1.transform.position.x - spawn5.transform.position.x) + (player1.transform.position.y - spawn5.transform.position.y)*(player1.transform.position.y - spawn5.transform.position.y)) >= spawnRange) 
					tooClose = true;
			}
			if(i == 6) {
				trans = spawn6.transform;
				if(Mathf.Sqrt((player1.transform.position.x - spawn6.transform.position.x)*(player1.transform.position.x - spawn6.transform.position.x) + (player1.transform.position.y - spawn6.transform.position.y)*(player1.transform.position.y - spawn6.transform.position.y)) >= spawnRange) 
					tooClose = true;
			}
			tooClose = false;
		} while (tooClose);

		var enemy = Instantiate (enemyToSpawn, trans.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = gameObject.transform;
		enemy.GetComponent<moveEnemy> ().tfPlayer = player1.transform;
	}
}
