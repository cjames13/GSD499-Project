﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : MonoBehaviour, StateController {
	// Layer name constants
	public const string DAMAGE_LAYER = "Damage";
	public const string SHOOT_LAYER  = "Shooting";
	public const string SHOOT_ANIM   = "Shooting Pistol";
	public const string RIFLE_ANIM   = "Shooting Rifle";
	public const string THROW_LAYER  = "Throwing";
	public const string THROW_ANIM   = "Throwing";
	public const string MELEE_LAYER  = "Meleeing";
	public const string MELEE_ANIM   = "Meleeing";
	public const string IDLE_MELEE_LAYER = "Idle Meleeing";
	public const string IDLE_MELEE_ANIM_1 = "Melee1";
	public const string IDLE_MELEE_ANIM_2 = "Melee2";
	private bool idle = false;
	private PlayerController playerController;
	private GameController gameController;

	// Movement state
	private Rigidbody rigidBody;
	private Rigidbody[] rigidBodies;

	private CapsuleCollider myCollider;
	private List<Collider> colliders;
	private Animator anim;

    // Damage Effect
    private float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Image damageImage;

    bool damaged;

    void Start() {
		gameObject.GetComponent<SphereCollider> ().enabled = false;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		anim = GetComponent<Animator> ();
		// All layers need to have their weight set via code for whatever reason
		anim.SetLayerWeight (1, 1f);
		anim.SetLayerWeight (2, 1f);
		anim.SetLayerWeight (3, 1f);
		anim.SetLayerWeight (4, .8f);
		anim.SetLayerWeight (5, 1f);
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

    void Update() {
		damageImage.color = (damaged) ? flashColor : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damaged = false;
		if (anim.GetFloat ("VerticalVelocity") < 0.01f && anim.GetFloat ("HorizontalVelocity") < 0.01f)
			idle = true;
		else
			idle = false;
    }

	void SetAllChildCollidersTrigger(bool t) {
		foreach (Collider c in GetComponentsInChildren<Collider>()) {
			if (c != myCollider)
				c.isTrigger = t;
		}
	}

	public void TakeDamage() {
        damaged = true;
		StartCoroutine (TimedDamageAnimation ());
	}

	IEnumerator TimedDamageAnimation() {
		anim.SetBool ("hurting", true);
		yield return new WaitForSeconds(1);
		anim.SetBool ("hurting", false);
	}

	public void Die() {
		myCollider.enabled = false;
		rigidBody.useGravity = false;
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
		}

		SetAllChildCollidersTrigger (false);
		anim.enabled = false;

		playerController.alive = false;
		gameController.LevelOver (true);
	}

	public void MeleeAttack(bool attacking){
		if (!IsAnimationPlaying (MELEE_LAYER, MELEE_ANIM) && attacking && !idle) {
				anim.SetTrigger ("melee");
		}
		else if (!IsAnimationPlaying (IDLE_MELEE_LAYER, IDLE_MELEE_ANIM_1) && idle == true && attacking) {
			Debug.Log ("a");
			anim.SetTrigger ("melee1");
		} 
		else if (IsAnimationPlaying (IDLE_MELEE_LAYER, IDLE_MELEE_ANIM_1) && idle == true && attacking) {
			Debug.Log ("b");
			anim.SetTrigger ("melee2");
		} 
		if (!IsAnimationPlaying (IDLE_MELEE_LAYER, IDLE_MELEE_ANIM_1) && !IsAnimationPlaying (IDLE_MELEE_LAYER, IDLE_MELEE_ANIM_2))
			playerController.moveSpeed = 4;
		else
			playerController.moveSpeed = 1;
	}

	public void RangedAttack(bool attacking, bool isRifle) {
		anim.SetBool ("shooting", attacking);
		anim.SetBool("rifle", (attacking && isRifle));
	}

	public void ThrownAttack(bool attacking) {
		if (!anim.GetBool("throw") && attacking) {
			anim.SetTrigger ("throw");
		}
	}

	// TODO: Move movement animation logic to states
	public void Walk(){}

	public bool IsAnimationPlaying(string layerName, string animationName) {
		return anim.GetCurrentAnimatorStateInfo (anim.GetLayerIndex (layerName)).IsName (animationName);
	}
}
