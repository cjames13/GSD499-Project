using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageOnContact : MonoBehaviour {
	public int damage = 1;
	public bool damageEverything = false;
	public List<string> damageObjectsWithTag;
	public bool destroySelfOnContact = false;

	void OnTriggerEnter(Collider other) {
		Damage (other);
	}

	void OnTriggerStay(Collider other) {
		Damage (other);
	}

	void Damage(Collider other) {
		Health health = other.GetComponent<Health> ();
		if (health != null) {
			if (damageEverything || damageObjectsWithTag.Contains (other.tag)) {
				health.Damage (damage);
				if (destroySelfOnContact) {
					Destroy (gameObject);
				}
			}
		}
	}
}
