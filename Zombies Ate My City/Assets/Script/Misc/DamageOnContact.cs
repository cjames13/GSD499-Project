using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageOnContact : MonoBehaviour {
	public int damage = 1;
	public bool damageEverything = false;
	public List<string> damageObjectsWithTag;
	public bool destroySelfOnContact = false;
	private Animator anim;
	bool NoDamageOnStay = false;
	void OnTriggerEnter(Collider other) {
			NoDamageOnStay = true;
			Damage (other);
	}

	void OnTriggerStay(Collider other) {
			NoDamageOnStay = false;
			Damage (other);
	}

	void Damage(Collider other) {
		Health health = other.GetComponent<Health> ();

		if (health != null) {
				
			if (damageEverything || damageObjectsWithTag.Contains (other.tag)) {
				if (other.GetComponent<Animator> () != null) {
					anim = other.GetComponent<Animator> ();
					if (other.tag == "Player") {
						if (anim.GetCurrentAnimatorStateInfo (4).IsName ("Not Meleeing"))
							health.Damage (damage);
					}
				
					if (gameObject.tag == "Enemy") {
						if (gameObject.GetComponent<Health> () != null &&
						   anim.GetCurrentAnimatorStateInfo (4).IsName ("Meleeing") && NoDamageOnStay)
							gameObject.GetComponent<Health> ().Damage (5);	
					}
				}
				if (other.tag != "Player")
				health.Damage (damage);
				if (destroySelfOnContact) {
					Destroy (gameObject);
				}
			}
		}
	}
}
