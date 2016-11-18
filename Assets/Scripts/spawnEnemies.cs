using UnityEngine;
using System.Collections;

public class spawnEnemies : MonoBehaviour {

	Main main;

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

	float x1;
	float y1;
	float x2;
	float y2;
	float x3;
	float y3;
	float x4;
	float y4;
	float x5;
	float y5;
	float x6;
	float y6;

	Transform trans;

	// Use this for initialization
	void Start () {
		main = GameObject.Find("Camera").GetComponent<Main>();
		trans = spawn1.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnEnemy(){
		int i;
		bool tooClose;
		float pX = player1.transform.position.x;
		float pY = player1.transform.position.y;

		x1 = spawn1.transform.position.x;
		y1 = spawn1.transform.position.y;
		x2 = spawn2.transform.position.x;
		y2 = spawn2.transform.position.y;
		x3 = spawn3.transform.position.x;
		y3 = spawn3.transform.position.y;
		x4 = spawn4.transform.position.x;
		y4 = spawn4.transform.position.y;
		x5 = spawn5.transform.position.x;
		y5 = spawn5.transform.position.y;
		x6 = spawn6.transform.position.x;
		y6 = spawn6.transform.position.y;

		int k = 0;

		do {
		tooClose = false;
		i = Random.Range (1, 6);
			if(i == 1) {
				trans = spawn1.transform;
				if(Mathf.Sqrt((pX-x1)*(pX-x1) + (pY-y1)*(pY-y1)) <= spawnRange) tooClose = true;
			}
			if(i == 2) {
				trans = spawn2.transform;
				if(Mathf.Sqrt((pX-x2)*(pX-x2) + (pY-y2)*(pY-y2)) <= spawnRange)	tooClose = true;
			}
			if(i == 3) {
				trans = spawn3.transform;
				if(Mathf.Sqrt((pX-x3)*(pX-x3) + (pY-y3)*(pY-y3)) <= spawnRange) tooClose = true;
			}
			if(i == 4) {
				trans = spawn4.transform;
				if(Mathf.Sqrt((pX-x4)*(pX-x4) + (pY-y4)*(pY-y4)) <= spawnRange)	tooClose = true;
			}
			if(i == 5) {
				trans = spawn5.transform;
				if(Mathf.Sqrt((pX-x5)*(pX-x5) + (pY-y5)*(pY-y5)) <= spawnRange)	tooClose = true;
			}
			if(i == 6) {
				trans = spawn6.transform;
				if(Mathf.Sqrt((pX-x6)*(pX-x6) + (pY-y6)*(pY-y6)) <= spawnRange)	tooClose = true;
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
		main.playerTrans = player1.transform;
	}
}
