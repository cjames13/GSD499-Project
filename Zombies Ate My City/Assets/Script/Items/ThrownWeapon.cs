using UnityEngine;
using System.Collections;

public class ThrownWeapon : RangedWeapon {
	public float throwDelay = 0.8f;

	public override void Attack() {
		attackTime -= Time.deltaTime;
		if (attackTime <= 0f && currentAmmo > 0) {
			StartCoroutine (ThrowDelayedProjectile ());
			currentAmmo--;
			attackTime = attackSpeed;
		}
	}

	IEnumerator ThrowDelayedProjectile() {
		yield return new WaitForSeconds (throwDelay);
		Instantiate (projectileObject, attackLocation.position, attackLocation.rotation);
	}

	public override void PlayAnimation(StateController stateController, bool attacking) {
		if(!OnCooldown()) stateController.ThrownAttack (attacking);
	}
}
