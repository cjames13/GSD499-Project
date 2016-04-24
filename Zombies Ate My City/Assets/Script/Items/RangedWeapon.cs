using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon {
	public GameObject projectileObject;
	public bool isThrownWeapon = false;
	public GameObject muzzle;
	public int maxAmmo;
	public int currentAmmo;
	private GameObject lightObject;
	private Light pointLight;
	private ParticleSystem muzzleFlash;
	private AudioSource muzzleSound;

	void Start() {
		if (muzzle != null) {
			muzzleFlash = muzzle.GetComponent<ParticleSystem> ();
			muzzleSound = muzzle.GetComponent<AudioSource> ();
			lightObject = GameObject.FindGameObjectWithTag ("MuzzleLight");
			pointLight = lightObject.GetComponent<Light> ();
		}

		attackLocation = GameObject.FindGameObjectWithTag ("AttackLocation").transform;
		currentAmmo = maxAmmo;
	}

	public override void Attack() {
		attackTime -= Time.deltaTime;
		pointLight.enabled = false;
		if (attackTime <= 0 && currentAmmo > 0) {
			Instantiate (projectileObject, attackLocation.position, attackLocation.rotation);
			currentAmmo--;
			attackTime = attackSpeed;
			pointLight.enabled = true;
			if (muzzle != null) {
				MuzzleFlash ();
			}
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
		muzzleFlash.Play ();
		muzzleSound.Play ();
	}

	public void IncreaseAmmo(int n) {
		currentAmmo = (currentAmmo + n >= maxAmmo) ? maxAmmo : currentAmmo + n;
	}
}
