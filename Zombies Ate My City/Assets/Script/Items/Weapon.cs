using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject projectileObject;

	private GameObject player;
	private Animator anim;
	public float firingSpeed = 0.125f;
	private float firingTime = 0f;
	private float hitTime = 0f;
	bool thrown = false;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator> ();
	}
	public void Fire(Transform firingLocation) {
		firingTime -= Time.deltaTime;
		if (firingTime <= 0) {
			Instantiate (projectileObject, firingLocation.position, firingLocation.rotation);
			firingTime = firingSpeed;
		}
	}

	public void ThrowOrHit(Transform firingLocation) {
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (5);
		PlayerController playerController = player.GetComponent<PlayerController> ();
		hitTime += Time.deltaTime;
		if (projectileObject) {
			if (thrown == false) {
				if (anim.GetBool ("running") == true || anim.GetBool ("jumping") == true)
					anim.speed = 1f;
				else
					anim.speed = 2f;
				anim.SetLayerWeight (5, 1);
				Instantiate (projectileObject, firingLocation.position, firingLocation.rotation);
				thrown = true;
			} else {
				if (hitTime > 0.8f) {
					hitTime = 0f;
					anim.speed = 1f;
					thrown = false;
					playerController.swing = false;
					anim.SetLayerWeight (5, 0);
				}
			}
		} else {
			if (thrown == false) {
				if (anim.GetBool ("running") == true || anim.GetBool ("jumping") == true)
					anim.speed = 0.8f;
				else
					anim.speed = 1.4f;
				anim.SetLayerWeight (7, 1);
				thrown = true;
			} else {
				if (hitTime > 0.8f) {
					hitTime = 0f;
					anim.speed = 0.8f;
					thrown = false;
					playerController.swing = false;
					anim.SetLayerWeight (7, 0);
				}
			}
		}
	}

}
