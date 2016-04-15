using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {
	Transform player;
	Animator anim;
	float time = 5;
	void Start(){
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
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
		time -= Time.deltaTime;
		if (time <= 2) {
			anim.SetLayerWeight (3, 1);
			if (time <= 0)
				time = 5;
		} else
			anim.SetLayerWeight (3, 0);
	}
	void StateController.Walk(){
		anim.SetBool ("walking", true);
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