using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject projectileObject;
	public Transform firePoint;
	public int   projectileDamage = 1;
	public float projectileSpeed  = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (projectileObject, firePoint.position, firePoint.transform.rotation);
		}
	}
}
