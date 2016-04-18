using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {

	Transform player;
	Animator anim;
	bool playing = true;
	bool shooting = true;
	public float magicAttackDelay = 7f;
	public float magicAttackTime;
	void Start(){
		anim = GetComponent<Animator> ();
		magicAttackTime = magicAttackDelay;
	}
	void StateController.TakeDamage() {
		// Damage taken animation here
		StartCoroutine (SetDamageLayerWeight ());
	}

	void StateController.Die() {
		/*myCollider.enabled = false;
		rigidBody.useGravity = false;
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
		}
		anim.enabled = false;
		SetAllChildCollidersTrigger (false);*/
		Destroy (gameObject);
	}
	IEnumerator SetDamageLayerWeight() {
		anim.SetLayerWeight (1, 1);
		yield return new WaitForSeconds(1);
		anim.SetLayerWeight (1, 0);
	}
	void StateController.Attack(bool attacking){
		if (anim.GetLayerWeight (1) == 1)
			attacking = false;
		if (attacking) {
			anim.SetLayerWeight (2, 1);
			if (anim.layerCount > 3)
				anim.SetLayerWeight (3, 0);
		} else {
			anim.SetLayerWeight (2, 0);
		}
	}

	void StateController.RangedAttack(bool attacking){
		if (anim.GetLayerWeight (1) == 1)
			attacking = false;
		if (attacking) {
			magicAttackTime -= Time.deltaTime;
			if (magicAttackTime <= 2) {
				AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (3);
				float playbackTime = currentState.normalizedTime % 1;
				if (playbackTime > 0.9f)
					playing = false;
				if (playing == true) {
					
					if (shooting == true) {
						GameObject fireBolt = (GameObject)Instantiate (Resources.Load ("Firebolt"),
							                     new Vector3 (transform.position.x, transform.position.y + 0.5f,
								transform.position.z - 0.5f), transform.rotation);
						shooting = false;
					}
					anim.SetLayerWeight (3, 1);
				}
				else
					anim.SetLayerWeight (3, 0);
				if (magicAttackTime <= 0) {
					playing = true;
					shooting = true;
					magicAttackTime = magicAttackDelay;
				}
			} else {
				anim.SetLayerWeight (3, 0);
			}
		} else {
			anim.SetLayerWeight (3, 0);
		}
	}

	void StateController.Walk(){
		anim.SetBool ("walking", true);
	}
		
}