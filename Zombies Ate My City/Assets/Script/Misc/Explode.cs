﻿

using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	private GameObject explosion;
	void Start()
	{
		explosion = Resources.Load ("FireExplosion") as GameObject;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ground" || other.tag == "Enemy") {
			Instantiate (explosion,
				new Vector3 (transform.position.x, transform.position.y,
					transform.position.z), transform.rotation);
			Destroy (gameObject);
		}

	}
}
	
