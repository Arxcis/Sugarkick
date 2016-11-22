using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

	public string nextLevel;

	public int waveNumber = 0;

	public Wave[] waves;

	// Use this for initialization
	void Start () {
		spawnEnemies = gameObject.GetComponent<SpawnEnemies> ();
		if(spawnEnemies.endlessMode == false) NextWave ();
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
			SceneManager.LoadScene(nextLevel);
		}
	}
		
	public void SetWave(int waveNum){

		spawnEnemies.enemiesToSpawn.Clear();

		for (int i = 0; i < waves[waveNum-1].enemiesToSpawn.Length; i++) {
				spawnEnemies.enemiesToSpawn.Add (waves [waveNum-1].enemiesToSpawn [i]);
		}

		spawnEnemies.spawns.Clear();

		for (int i = 0; i < waves [waveNum - 1].spawns.Length; i++) {
				spawnEnemies.spawns.Add( waves [waveNum-1].spawns [i]);
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
