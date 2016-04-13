using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject projectileObject;
	public Transform firePoint;
	public int   projectileDamage = 1;
	public float projectileSpeed  = 5;
	GameObject player;
	Animator anim;
	float currentWeight = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator> ();
	}
	void Update () {

		//		if (Input.GetButtonDown ("Fire1") && currentWeight == 1) {
		//			Instantiate (projectileObject, firePoint.position, firePoint.transform.rotation);
		//		} 
		if (Input.GetButton ("Fire1")) {
			currentWeight = Mathf.Lerp(currentWeight,1.0f,Time.deltaTime * 20);
			anim.SetLayerWeight(2, currentWeight);
			if (currentWeight >= 0.9f)
				Instantiate (projectileObject, firePoint.position, firePoint.transform.rotation);
		} else {
			currentWeight = Mathf.Lerp(currentWeight,0.0f,Time.deltaTime * 20); 
			anim.SetLayerWeight(2, currentWeight);
		}
	}
}


