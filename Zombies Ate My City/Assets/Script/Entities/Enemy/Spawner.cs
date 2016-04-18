using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public GameObject monsterObject;
	public bool randomSpawnTime;

	[Range(0.0f, 1.0f)]
	public float randomSpawnChance;
	public float maximumSpawnTime;
	[Tooltip("Only used if randomSpawnChance is true")]
	public float minimumSpawnTime;
	[Tooltip("Only used if randomSpawnChance is true")]
	public float timeBetweenTries;

	[Tooltip("Set to zero for no maximum")]
	public int maxEnemiesSpawned = 0;

	private float lastSpawnTime, lastTry;
	private SphereCollider spawnRadius;
	private List<GameObject> spawnedMonsters = new List<GameObject>();

	void Start () {
		lastTry = lastSpawnTime = Time.time;
		spawnRadius = GetComponent<SphereCollider>();
	}

	void Update () {
		float secondsSinceSpawn = Time.time - lastSpawnTime;
		CullDeadMonsters ();

		if (spawnedMonsters.Count < maxEnemiesSpawned || maxEnemiesSpawned <= 0) {
			if (secondsSinceSpawn > maximumSpawnTime) {
				Spawn ();
			} else if (randomSpawnTime) {
				if (secondsSinceSpawn > minimumSpawnTime) {
					if (Time.time - lastTry > timeBetweenTries) {
						if (Random.Range (0f, 1f) <= randomSpawnChance) {
							Spawn ();
						}
					}
				}
			}
		}
	}

	void CullDeadMonsters() {
		spawnedMonsters.RemoveAll (a => a == null);
	}

	void Spawn() {
		lastSpawnTime = Time.time;
		float x = Random.Range (spawnRadius.transform.position.x - spawnRadius.radius / 2, 
			spawnRadius.transform.position.x + spawnRadius.radius / 2);
		float z = Random.Range (spawnRadius.transform.position.z - spawnRadius.radius / 2, 
			spawnRadius.transform.position.z + spawnRadius.radius / 2);
		Vector3 spawnLocation = new Vector3(x, spawnRadius.transform.position.y, z);
		spawnedMonsters.Add((GameObject)Instantiate (monsterObject, spawnLocation, Quaternion.identity));
	}
}
