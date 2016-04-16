using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject monsterObject;
	public bool randomSpawnTime;

	[Range(0.0f, 1.0f)]
	public float randomSpawnChance;
	public float maximumSpawnTime;
	public float minimumSpawnTime;
	public float timeBetweenTries;

	private float lastSpawnTime, lastTry;
	private SphereCollider spawnRadius;

	void Start () {
		lastTry = lastSpawnTime = Time.time;
		spawnRadius = GetComponent<SphereCollider>();
	}

	void Update () {
		float secondsSinceSpawn = Time.time - lastSpawnTime;

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

	void Spawn() {
		lastSpawnTime = Time.time;
		float x = Random.Range (spawnRadius.transform.position.x - spawnRadius.radius / 2, 
			spawnRadius.transform.position.x + spawnRadius.radius / 2);
		float z = Random.Range (spawnRadius.transform.position.z - spawnRadius.radius / 2, 
			spawnRadius.transform.position.z + spawnRadius.radius / 2);
		Vector3 spawnLocation = new Vector3(x, spawnRadius.transform.position.y, z);
		Instantiate (monsterObject, spawnLocation, Quaternion.identity);
	}
}
