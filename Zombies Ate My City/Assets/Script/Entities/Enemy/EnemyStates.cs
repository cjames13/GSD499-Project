using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {
	int health = 1;
	Rigidbody rigidBody;
	Rigidbody[] rigidBodies;
	CapsuleCollider myCollider;
	BoxCollider myBoxCollider;
	SphereCollider mySphereCollider;
	List<Collider> colliders;
	Animator anim;
	StateController stateController;
	void Awake(){
		if (GetComponent<BoxCollider> ())
			myBoxCollider = GetComponent<BoxCollider> ();
		if (GetComponent<SphereCollider> ())
			mySphereCollider = GetComponent<SphereCollider> ();
		anim = GetComponent<Animator> ();
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
			if (c != myCollider && c != myBoxCollider && c != mySphereCollider)
				c.isTrigger = t;
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "PlayerAttack") {
			stateController.TakeDamage ();
		}
	}
	void StateController.TakeDamage() {
		health -= 1;
		if (health == 0) {
			stateController.Die ();
			SetAllChildCollidersTrigger (false);
		}
		// Damage taken animation here
	}

	void StateController.Die() {
		myCollider.enabled = false;
		rigidBody.useGravity = false;
		foreach (Rigidbody rb in rigidBodies) {
			rb.isKinematic = false;
		}
		anim.enabled = false;
		SetAllChildCollidersTrigger (false);
		//Destroy (gameObject);
	}

}


//void StateController.TakeDamage() {
// Damage taken animation here
//}

//void StateController.Die() {
//	Destroy (gameObject);
//}