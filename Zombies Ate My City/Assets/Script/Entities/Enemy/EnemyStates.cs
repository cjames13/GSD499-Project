using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {
	public float magicAttackDelay = 5f;

	Transform player;
	Animator anim;

	public float magicAttackTime;

	void Start(){
		anim = GetComponent<Animator> ();
		magicAttackTime = magicAttackDelay;
	}
	void StateController.TakeDamage() {
		// Damage taken animation here
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
	void StateController.Attack(NavMeshAgent agent){
		anim.SetLayerWeight (2, 1);
	}

	void StateController.MagicAttack(NavMeshAgent agent){
		magicAttackTime -= Time.deltaTime;
		if (magicAttackTime <= 2) {
			anim.SetLayerWeight (3, 1);
			if (magicAttackTime <= 0)
				magicAttackDelay = 5;
		} else
			anim.SetLayerWeight (3, 0);
	}

	void StateController.Walk(){
		//anim.SetBool ("walking", true);
	}

	void StateController.Run(){
		
	}
}


//void StateController.TakeDamage() {
// Damage taken animation here
//}

//void StateController.Die() {
//	Destroy (gameObject);
//}