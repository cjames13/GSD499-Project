using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {

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

}


//void StateController.TakeDamage() {
// Damage taken animation here
//}

//void StateController.Die() {
//	Destroy (gameObject);
//}