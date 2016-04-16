using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageOnContact : MonoBehaviour {
	public int damage = 1;
	public bool damageEverything = false;
	public List<string> damageObjectsWithTag;

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponent<Health> ();
		if (health != null) {
			if (damageEverything || damageObjectsWithTag.Contains (other.tag)) {
				health.Damage (damage);
			}
		}
	}
}
