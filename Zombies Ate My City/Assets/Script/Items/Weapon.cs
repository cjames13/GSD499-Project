using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject projectileObject;
	public float firingSpeed = 0.125f;

	private float firingTime = 0f;

	public void Fire(Transform firingLocation) {
		firingTime -= Time.deltaTime;

		if (firingTime <= 0) {
			Instantiate (projectileObject, firingLocation.position, firingLocation.rotation);
			firingTime = firingSpeed;
		}
	}
}
