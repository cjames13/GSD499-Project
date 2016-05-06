using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {
	private Collider damageCollider;

	void Start() {
		damageCollider = GetComponent<Collider> ();
	}

	public override void Attack() {}

	public override void PlayAnimation(StateController stateController, bool attacking) {
		stateController.MeleeAttack (attacking);
		damageCollider.enabled = stateController.IsAnimationPlaying (PlayerStates.MELEE_LAYER, PlayerStates.MELEE_ANIM1) ||
			stateController.IsAnimationPlaying (PlayerStates.MELEE_LAYER, PlayerStates.MELEE_ANIM2) ||
			stateController.IsAnimationPlaying (PlayerStates.IDLE_MELEE_LAYER, PlayerStates.MELEE_ANIM1) ||
			stateController.IsAnimationPlaying (PlayerStates.IDLE_MELEE_LAYER, PlayerStates.MELEE_ANIM2);
	}

}
