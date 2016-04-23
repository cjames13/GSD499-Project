using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon {
	public GameObject projectileObject;
	public bool isThrownWeapon = false;
	GameObject muzzle;
	GameObject muzzleLight;
	AudioSource muzzleSound;
	GameObject player;
	Animator anim;
	void Start(){
		muzzle = GameObject.FindGameObjectWithTag ("Muzzle");
		muzzleLight = GameObject.FindGameObjectWithTag ("MuzzleLight");
		muzzleSound = muzzle.GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator> ();
		muzzleSound.playOnAwake = true;
	}
	public override void Attack() {
		attackTime -= Time.deltaTime;
		if (anim.GetBool ("shooting") == true && attackTime < 0.05)
			muzzleLight.SetActive (true);
		else
			muzzleLight.SetActive (false);
		if (attackTime <= 0) {
			Instantiate (projectileObject, attackLocation.position, attackLocation.rotation);
			attackTime = attackSpeed;
			muzzle.SetActive (false);
			muzzle.SetActive (true);

		}
	}

	public override void PlayAnimation(StateController stateController, bool attacking) {
		if (isThrownWeapon) {
			stateController.ThrownAttack (attacking);
		} else {
			stateController.RangedAttack (attacking);
		}
	}
}
