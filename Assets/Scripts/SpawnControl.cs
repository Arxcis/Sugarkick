using UnityEngine;
using System.Collections;

public class SpawnControl : MonoBehaviour {
	public bool startWave = false;
	SpawnEnemies spawnEnemies;
	[System.Serializable]
	public class Wave
	{
		public GameObject[] enemiesToSpawn;
		public int spawnRange = 0;
		public bool endlessMode = false;
		public bool staticSpawner = false;
		public bool waveMode = false;
		public float spawnsPerSecond;
		public int numberOfEnemies;
		public int maxEnemiesOnScreen;
		public GameObject[] spawns;
	}

	public int waveNumber = 0;

	public Wave[] waves;

	// Use this for initialization
	void Start () {
		spawnEnemies = gameObject.GetComponent<SpawnEnemies> ();
		NextWave ();
	}
	
	// Update is called once per frame
	void Update () {
		if (startWave) NextWave ();
	}

	public void NextWave(){
		if (waveNumber < waves.Length) {
			waveNumber++;
			SetWave (waveNumber);
		}
		else {
			print ("Last wave complete!");
		}
	}
		
	public void SetWave(int waveNum){
		
		for (int i = 0; i < waves[waveNum-1].enemiesToSpawn.Length; i++) {
			if(i < spawnEnemies.enemiesToSpawn.Length){
				spawnEnemies.enemiesToSpawn [i] = waves [waveNum-1].enemiesToSpawn [i];
			}
		}

		for (int i = 0; i < waves [waveNum - 1].spawns.Length; i++) {
			if(i < spawnEnemies.spawns.Length){
				spawnEnemies.spawns [i] = waves [waveNum-1].spawns [i];
			}
		}

		spawnEnemies.spawnRange = waves [waveNum-1].spawnRange;
		spawnEnemies.endlessMode = waves [waveNum-1].endlessMode;
		spawnEnemies.staticSpawner = waves [waveNum-1].staticSpawner;
		spawnEnemies.waveMode = waves [waveNum-1].waveMode;
		spawnEnemies.spawnsPerSecond = waves [waveNum-1].spawnsPerSecond;
		spawnEnemies.numberOfEnemies = waves [waveNum-1].numberOfEnemies;
		spawnEnemies.maxEnemiesOnScreen = waves [waveNum-1].maxEnemiesOnScreen;
	}
}
