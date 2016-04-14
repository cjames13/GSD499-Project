using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject projectileObject;
	public Transform firePoint;
	public float firingSpeed = 0.125f;
	public float drawTime = 20f;
	public float holsterTime = 10f;

	Animator anim;
	float currentWeight = 0;
	float time = 0;

	float animationTime;
	bool draw = false;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetButton ("Fire1")) {
			time -= Time.deltaTime;

			if (time <= 0) {
				Instantiate (projectileObject, firePoint.position, firePoint.transform.rotation);
				time = firingSpeed;
			}

			if (!draw) {
				draw = true;
				animationTime = 0f;
			}
		} else {
			draw = false;
		}

		animationTime += Time.deltaTime;
		float thisTime = (draw) ? drawTime : holsterTime;

		if (animationTime > thisTime) {
			animationTime = thisTime;
		}

		currentWeight = Mathf.Lerp (currentWeight, (draw) ? 1.0f : 0.0f, animationTime / thisTime);
		anim.SetLayerWeight(2, currentWeight);
	}
}
