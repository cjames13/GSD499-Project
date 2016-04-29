

using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	public GameObject explosion;

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Prop" || other.transform.tag == "Enemy") {
			Instantiate (explosion,
				new Vector3 (transform.position.x, transform.position.y,
					transform.position.z), transform.rotation);
			Destroy (gameObject);
		}

	}
}
	
