using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	Main main;

	public GameObject[] enemiesToSpawn;
	public float spawnRange;

	public bool endlessMode = false;
	public bool staticSpawner = false;
	public bool waveMode = false;
	public float spawnsPerSecond;
	public int numberOfEnemies;
	public int maxEnemiesOnScreen;

	public GameObject player1;
	public GameObject player2;

	public GameObject[] spawns;
	public float[] xPos;
	public float[] yPos;
	Transform trans;
	float staticSpawnerTimer = 0.0F;

	int enemiesSpawned = 0;
	int currentEnemyCount = 0;
	// Use this for initialization
	void Start () {
		// main = GameObject.Find("Camera").GetComponent<Main>();  // Not used
		trans = spawns[0].transform;
	}

	public void gotKilled (string tag){
		if (endlessMode) {
			if (tag == "Enemy1") spawnEnemy (1);
			if (tag == "Enemy2") spawnEnemy (2);
			if (tag == "Enemy3") spawnEnemy (3);
			if (tag == "Enemy4") spawnEnemy (4);
		}
		currentEnemyCount--;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (staticSpawner || (waveMode && enemiesSpawned < numberOfEnemies && currentEnemyCount < maxEnemiesOnScreen)) {
			staticSpawnerTimer += Time.deltaTime*spawnsPerSecond;
			if (staticSpawnerTimer >= 1) {
				spawnEnemy (0);
				staticSpawnerTimer--;
				if (waveMode) {
					enemiesSpawned++;
					currentEnemyCount++;
				}
			}
		}
		if (enemiesSpawned >= numberOfEnemies) {
			waveMode = false;
			enemiesSpawned = 0;
			staticSpawnerTimer = 0.0F;
		}
	}

	public void spawnEnemy(int enemyType){
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

		var enemy = Instantiate (enemiesToSpawn[enemyType], trans.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = gameObject.transform;
	}
}
