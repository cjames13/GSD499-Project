﻿using UnityEngine;
public class HealthPickup : MonoBehaviour {
	public int amount;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<Health>().currentHealth += amount;
			Destroy (gameObject);
		}
	}
}