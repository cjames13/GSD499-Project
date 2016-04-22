using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon {
	public GameObject projectileObject;
	public bool isThrownWeapon = false;

	public override void Attack() {
		attackTime -= Time.deltaTime;
		if (attackTime <= 0) {
			Instantiate (projectileObject, attackLocation.position, attackLocation.rotation);
			attackTime = attackSpeed;
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
