using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon {
	public GameObject projectileObject;
	public bool isThrownWeapon = false;
	public GameObject muzzle;
	public int maxAmmo;
	public int currentAmmo;

	private ParticleSystem muzzleFlash;
	private AudioSource muzzleSound;

	void Start() {
		if (muzzle != null) {
			muzzleFlash = muzzle.GetComponent<ParticleSystem> ();
			muzzleSound = muzzle.GetComponent<AudioSource> ();
		}

		attackLocation = GameObject.FindGameObjectWithTag ("AttackLocation").transform;
		currentAmmo = maxAmmo;
	}

	public override void Attack() {
		attackTime -= Time.deltaTime;

		if (attackTime <= 0 && currentAmmo > 0) {
			Instantiate (projectileObject, attackLocation.position, attackLocation.rotation);
			currentAmmo--;
			attackTime = attackSpeed;
			MuzzleFlash ();
		}
	}

	public override void PlayAnimation(StateController stateController, bool attacking) {
		if (isThrownWeapon) {
			stateController.ThrownAttack (attacking);
		} else {
			stateController.RangedAttack (attacking);
		}
	}

	void MuzzleFlash() {
		if (muzzleFlash != null) {
			muzzleFlash.Play ();
		}

		if (muzzleSound != null) {
			muzzleSound.Play ();
		}
	}

	public void IncreaseAmmo(int n) {
		currentAmmo = (currentAmmo + n >= maxAmmo) ? maxAmmo : currentAmmo + n;
	}
}
