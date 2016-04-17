using UnityEngine;
using System.Collections;

public class Projectile : DamageOnContact {
	public float 	speed;
	public float	timeToLive = 4f;

	private float creationTime;

	void Start() {
		creationTime = Time.time;
		base.destroySelfOnContact = true;
	}

	void Update() {
		if (Time.time - creationTime > timeToLive) {
			Destroy (gameObject);
		}

		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
}
