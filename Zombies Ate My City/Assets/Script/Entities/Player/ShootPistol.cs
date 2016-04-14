using UnityEngine;
using System.Collections;

public class ShootPistol : MonoBehaviour {
	public GameObject projectileObject;
	GameObject player;
	public Transform firePoint;
	Animator anim;
	float currentWeight = 0;
	public float rapidSpeed = 0.125f;
	float time = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Fire1")) {
			time -= Time.deltaTime;
			if (time <= 0) {
				Instantiate (projectileObject, firePoint.position, firePoint.transform.rotation);
				time = rapidSpeed;
			}
			currentWeight = Mathf.Lerp(currentWeight,1.0f,Time.deltaTime * 50);
			anim.SetLayerWeight(2, currentWeight);
		} else {
			currentWeight = Mathf.Lerp(currentWeight,0.0f,Time.deltaTime * 20); 
			anim.SetLayerWeight(2, currentWeight);
		}
	}
}



