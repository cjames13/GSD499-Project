using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {
	public float attackTimeLength = 3f;
	private Collider damageCollider;

	void Start() {
		damageCollider = GetComponent<Collider> ();
	}

	public override void Attack() {}

	public override void PlayAnimation(StateController stateController, bool attacking) {
		if (attackTime <= 0) {
			stateController.MeleeAttack (attacking);
			attackTime = attackSpeed;
		}

		damageCollider.enabled = stateController.IsAnimationPlaying (PlayerStates.MELEE_LAYER, PlayerStates.MELEE_ANIM);
		damageCollider.enabled = stateController.IsAnimationPlaying (PlayerStates.IDLE_MELEE_LAYER, PlayerStates.IDLE_MELEE_ANIM_1);
		damageCollider.enabled = stateController.IsAnimationPlaying (PlayerStates.IDLE_MELEE_LAYER, PlayerStates.IDLE_MELEE_ANIM_2);
	}

}
