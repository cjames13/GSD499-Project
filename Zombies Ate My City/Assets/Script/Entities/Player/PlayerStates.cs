using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour, StateController {
	private PlayerController playerController;
	WeaponController weaponController;
	// Movement state
	private Rigidbody rigidBody;
	private Rigidbody[] rigidBodies;

	private CapsuleCollider myCollider;
	private List<Collider> colliders;

	// Attack state
	public float weaponDrawTime = 20f;
	public float weaponHolsterTime = 10f;

	private Animator anim;
	private bool drawingWeapon = false;
	private float drawAnimationTime = 0f;
	private float currentWeight = 0f;

	void Start() {
		anim = GetComponent<Animator> ();
		playerController = GetComponent<PlayerController> ();
		weaponController = GetComponent<WeaponController> ();
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
		StartCoroutine (SetDamageLayerWeight ());
	}

	void StateController.Die() {
		myCollider.enabled = false;
		rigidBody.useGravity = false;
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
		}

		SetAllChildCollidersTrigger (false);
		anim.enabled = false;

		playerController.dead = true;
	}

	IEnumerator SetDamageLayerWeight() {
		anim.SetLayerWeight (1, 1);
		yield return new WaitForSeconds(1);
		anim.SetLayerWeight (1, 0);
	}

	void StateController.Attack(bool attacking){}

	void StateController.RangedAttack(bool attacking)
	{
		if (attacking) {
			if (!drawingWeapon) {
				drawingWeapon = true;
				drawAnimationTime = 0f;
			}
		} else {
			drawingWeapon = false;
		}

		drawAnimationTime += Time.deltaTime;
		float thisTime = (drawingWeapon) ? weaponDrawTime : weaponHolsterTime;

		if (drawAnimationTime > thisTime) {
			drawAnimationTime = thisTime;
		}

		currentWeight = Mathf.Lerp (currentWeight, (drawingWeapon) ? 1.0f : 0.0f, drawAnimationTime / thisTime);
		if (weaponController.weapons [weaponController.currentlyEquippedIndex].name == "Bit Gun")
			anim.SetLayerWeight (2, currentWeight);
		if (weaponController.weapons [weaponController.currentlyEquippedIndex].name == "Rifle")
			anim.SetLayerWeight (4, currentWeight);
	}

	// TODO: Move movement animation logic to states
	void StateController.Walk(){}

}
