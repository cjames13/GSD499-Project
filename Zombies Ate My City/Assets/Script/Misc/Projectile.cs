using UnityEngine;
using System.Collections;

public class Projectile : DamageOnContact {
	public float 	speed;
	public float	timeToLive = 4f;
	private float creationTime;
	public GameObject spark;
	void Start() {
		creationTime = Time.time;
	}

	void Update() {
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		if (Time.time - creationTime > timeToLive) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (speed > 40) {
			if (other.transform.tag == "Enemy") {
				Instantiate (spark, transform.position, other.transform.rotation);
			}
		}
	}
}
