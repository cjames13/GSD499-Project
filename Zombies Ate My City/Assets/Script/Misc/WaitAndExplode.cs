using UnityEngine;
using System.Collections;

public class WaitAndExplode : MonoBehaviour {
	public GameObject explosion;
	public float time = 1f;
	void Update()
	{
		time -= Time.deltaTime;
		if (time <= 0) {
			Instantiate (explosion,
				new Vector3 (transform.position.x, transform.position.y,
					transform.position.z), transform.rotation);
			Destroy (gameObject);
		}
	}

}
