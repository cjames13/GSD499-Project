using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {
	public override void Attack() {}

	public override void PlayAnimation(StateController stateController, bool attacking) {
		attackTime -= Time.deltaTime;
		if (attackTime <= 0) {
			stateController.MeleeAttack (attacking);
			attackTime = attackSpeed;
		}
	}

}
