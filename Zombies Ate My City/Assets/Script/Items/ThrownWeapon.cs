using UnityEngine;
using System.Collections;

public class ThrownWeapon : RangedWeapon {
	public float throwDelay = 0.8f;

	private bool hasThrown = false;

	public override void Attack() {
		if (!OnCooldown() && currentAmmo > 0) {
			hasThrown = false;
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
		if (!hasThrown) {
			stateController.ThrownAttack (attacking);
			hasThrown = true;
		}
	}
}
