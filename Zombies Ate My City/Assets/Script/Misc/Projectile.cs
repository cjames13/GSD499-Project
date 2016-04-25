using UnityEngine;
using System.Collections;

public class Projectile : DamageOnContact {
	public float 	speed;
	public float	timeToLive = 4f;
	public bool explosive = false;
	private float creationTime;

	void Start() {
		creationTime = Time.time;
		if (explosive == false)
		base.destroySelfOnContact = true;
	}

	void Update() {
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		if (Time.time - creationTime > timeToLive) {
			Destroy (gameObject);
		}

	}
}
