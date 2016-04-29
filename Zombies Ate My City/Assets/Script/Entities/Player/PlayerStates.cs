using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour, StateController {
	private PlayerController playerController;

	// Movement state
	private Rigidbody rigidBody;
	private Rigidbody[] rigidBodies;

	private CapsuleCollider myCollider;
	private List<Collider> colliders;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
		anim.SetLayerWeight (1, 1f);
		anim.SetLayerWeight (2, 1f);
		anim.SetLayerWeight (3, 1f);
		anim.SetLayerWeight (4, 1f);
		playerController = GetComponent<PlayerController> ();

		rigidBody = GetComponent<Rigidbody>();
		rigidBodies = GetComponentsInChildren<Rigidbody> ();
		myCollider = GetComponent<CapsuleCollider> ();

		foreach (Rigidbody rb in rigidBodies) {
			if (rb != rigidBody)
				rb.isKinematic = true;
		}

		rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

		SetAllChildCollidersTrigger (true);
	}

	void SetAllChildCollidersTrigger(bool t) {
		foreach (Collider c in GetComponentsInChildren<Collider>()) {
			if (c != myCollider)
				c.isTrigger = t;
		}
	}

	void StateController.TakeDamage() {
		StartCoroutine (TimedDamageAnimation ());
	}

	IEnumerator TimedDamageAnimation() {
		anim.SetBool ("hurting", true);
		yield return new WaitForSeconds(1);
		anim.SetBool ("hurting", false);
	}

	void StateController.Die() {
		myCollider.enabled = false;
		rigidBody.useGravity = false;
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
		}

		SetAllChildCollidersTrigger (false);
		anim.enabled = false;

		playerController.alive = false;
		StartCoroutine (RestartLevel ());
	}


	IEnumerator RestartLevel()
	{
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void StateController.MeleeAttack(bool attacking){
		if (attacking) {
			anim.SetTrigger ("melee");
		} else {
			anim.ResetTrigger ("melee");
		}
	}

	void StateController.RangedAttack(bool attacking, bool isRifle) {
		anim.SetBool ("shooting", attacking);
		anim.SetBool("rifle", ((anim.GetBool("walking") || anim.GetBool("running")) && isRifle));
	}

	void StateController.ThrownAttack(bool attacking) {
		if (attacking) {
			anim.SetTrigger ("throw");
		} else {
			anim.ResetTrigger ("throw");
		}
	}

	// TODO: Move movement animation logic to states
	void StateController.Walk(){}

}
