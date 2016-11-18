using UnityEngine;
using System.Collections;

public class spawnEnemies : MonoBehaviour {

	Main main;

	public GameObject enemyToSpawn;
	public float spawnRange;

	public GameObject player1;
	public GameObject player2;

	public GameObject[] spawns;
	public float[] xPos;
	public float[] yPos;
	Transform trans;

	// Use this for initialization
	void Start () {
		main = GameObject.Find("Camera").GetComponent<Main>();
		trans = spawns[0].transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnEnemy(){
		bool tooClose;
		float pX = player1.transform.position.x;
		float pY = player1.transform.position.y;

		for (int i = 0; i < spawns.Length; i++) {
			xPos[i] = spawns[i].transform.position.x;
			yPos[i] = spawns[i].transform.position.y;
		}

		int k = 0;

		do {
		tooClose = false;
			int j = Random.Range (0, spawns.Length-1);

			for (int f = 0; f < spawns.Length; f++) {
				if(j == f){
					trans = spawns[f].transform;
					if(Mathf.Sqrt((pX-xPos[f])*(pX-xPos[f]) + (pY-yPos[f])*(pY-yPos[f])) <= spawnRange) tooClose = true;
				}
			}
			k++;
			if(k >= 20){
				tooClose = false;
				print("Tried and failed 20 times.");
			}
			//tooClose = false;
		} while (tooClose);

		var enemy = Instantiate (enemyToSpawn, trans.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = gameObject.transform;
	}
}
