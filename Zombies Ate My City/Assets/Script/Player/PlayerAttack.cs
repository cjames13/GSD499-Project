using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject projectileObject;
	public Transform firePoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			// Shoot from end of gun
			Instantiate (projectileObject, firePoint.position, firePoint.transform.rotation);
			// Always shoot forward (for testing)
 			//Instantiate (projectileObject, firePoint.position, Quaternion.identity);
		}
	}
}
