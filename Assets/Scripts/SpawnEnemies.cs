using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour {

	Main main;
	SpawnControl spawnControl;

	public List<GameObject> enemiesToSpawn = new List<GameObject>();
	public float spawnRange;

	public bool endlessMode = false;
	public bool staticSpawner = false;
	public bool waveMode = false;
	public float spawnsPerSecond;
	public int numberOfEnemies;
	public int maxEnemiesOnScreen;

	public GameObject player1;
	public GameObject player2;

	public List<GameObject> spawns = new List<GameObject>();
	public float[] xPos;
	public float[] yPos;
	Transform trans;
	float staticSpawnerTimer = 0.0F;

	int enemiesSpawned = 0;
	public int currentEnemyCount = 0;

	// Use this for initialization
	void Start () {
		main = GameObject.Find("Camera").GetComponent<Main>();
		if(endlessMode == false) trans = spawns[0].transform;
		spawnControl = gameObject.GetComponent<SpawnControl> ();
	}

	// This function is called by dying enemies.
	public void gotKilled (string tag){
		main.NewScore ();

	// In endless mode, spawn a new enemy of the same type.
		if (endlessMode) {
			if (tag == "Enemy1") spawnEnemy (1);
			if (tag == "Enemy2") spawnEnemy (2);
			if (tag == "Enemy3") spawnEnemy (3);
			if (tag == "Enemy4") spawnEnemy (4);
		}

	// Tracks the amount of enemies currently alive on the map.
		currentEnemyCount--;
	}

	// Update is called once per frame
	void FixedUpdate () {

	// Will spawn enemies with a set interval. If wavemode, it makes sure that it does not spawn too many.
		if (staticSpawner || (waveMode && enemiesSpawned < numberOfEnemies && currentEnemyCount < maxEnemiesOnScreen)) {
			staticSpawnerTimer += Time.deltaTime*spawnsPerSecond;
			if (staticSpawnerTimer >= 1) {

	// Assigns a value to int j from amount of enemies in the enemypool.
				int j = Random.Range (0, enemiesToSpawn.Count);     // jonas: added minus 1 to prevent reference to invalid index
				spawnEnemy (j);
				staticSpawnerTimer--;

	// Track the amount of enemies currently alive on the map as well as keeping track of wave-progress.
				if (waveMode) {
					enemiesSpawned++;
					currentEnemyCount++;
				}
			}
		}
		if (endlessMode == false && enemiesSpawned >= numberOfEnemies && currentEnemyCount == 0) {
			waveMode = false;
			enemiesSpawned = 0;
			staticSpawnerTimer = 0.0F;
			print ("Currently " + currentEnemyCount + " enemies alive");
			spawnControl.NextWave ();
		}
	}


	// Spawns a new enemy of type "enemyType", passed by argument.
	public void spawnEnemy(int enemyType){
		bool tooClose;
		float pX = player1.transform.position.x;
		float pY = player1.transform.position.y;

	// Stores the spawn-positions in xPos and yPos-variables.
			for (int i = 0; i < spawns.Count; i++) {
				xPos [i] = spawns[i].transform.position.x;
				yPos [i] = spawns[i].transform.position.y;
			}

			int k = 0;

	// Loops until a fitting spawn-location is found.
			do {

	// This bool is used to make sure the enemy is spawned "spawnRange"-distance away from the player.
				tooClose = false;

	// This number is used to select a spawn-location from the "spawns"-list.
				int j = Random.Range (0, spawns.Count - 1);

	// Loops through the spawn-locations to see if number 'j' fits and is not "tooClose".
				for (int f = 0; f < spawns.Count; f++) {
					if (j == f) {
						trans = spawns [f].transform;
						if (Mathf.Sqrt ((pX - xPos [f]) * (pX - xPos [f]) + (pY - yPos [f]) * (pY - yPos [f])) <= spawnRange)
							tooClose = true;
					}
				}

	// This part is a failsafe which makes sure the program does not loop infinitely.
				k++;
				if (k >= 20) {
					tooClose = false;
					print ("Tried and failed 20 times.");
				}
				//tooClose = false;
			} while (tooClose);

	// Creates the enemy of the correct type in a fitting location, then parents it to the spawner.
		var enemy = Instantiate (enemiesToSpawn[enemyType], trans.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = gameObject.transform;
		if(enemy.tag.Contains("2")) enemy.GetComponentInChildren<PuppetManip>().isSpawnerChild = true;
		else enemy.GetComponent<PuppetManip>().isSpawnerChild = true;           //Sets the isSpawnerChild to true in the enemy's PupManip
	}
}
