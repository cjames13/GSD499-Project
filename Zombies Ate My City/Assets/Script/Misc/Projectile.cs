using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public int   	projectileDamage;
	public float 	projectileSpeed;
	public bool 	friendly = true;
	public float	timeToLive = 4f;

	private float creationTime;

	void Start() {
		creationTime = Time.time;
	}

	void Update() {
		if (Time.time - creationTime > timeToLive) {
			Destroy (gameObject);
		}

		transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
	}

	void OnTriggerEnter(Collider other) {
		if ((other.tag == "Player" && !friendly) || (other.tag == "Enemy" && friendly)) {
			other.GetComponent<Health> ().Damage (projectileDamage);
		} else if (!other.CompareTag ("Player") && !other.CompareTag ("Enemy")) {
			Destroy (gameObject);
		}
	}
}
