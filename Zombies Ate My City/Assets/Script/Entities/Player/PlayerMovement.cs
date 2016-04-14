

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement: MonoBehaviour {
	public float moveSpeed = 4f;
	public float jumpSpeed = 5f;
	Vector3 moveDirection;
	Animator anim;
	Rigidbody rigidBody;
	Rigidbody[] rigidBodies;

	CapsuleCollider myCollider;
	List<Collider> colliders;

	public bool dead = false;
	bool jumped = false;
	Transform firePoint;
	GameObject cameraObject;
	Camera cam;

	void Awake(){
		rigidBody = GetComponent<Rigidbody>();
		rigidBodies = GetComponentsInChildren<Rigidbody> ();
		myCollider = GetComponent<CapsuleCollider> ();
		firePoint = GameObject.FindGameObjectWithTag ("FiringPoint").transform;
		foreach (Rigidbody rb in rigidBodies) {
			if (rb != rigidBody)
				rb.isKinematic = true;
		}

		rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

		SetAllChildCollidersTrigger (true);

		anim = GetComponent<Animator> ();
		anim.enabled = true;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		cam = cameraObject.GetComponent<Camera> ();
	}

	void FixedUpdate() {
		if (IsGrounded ()) {
			jumped = false;
			firePoint.rotation = transform.rotation;
		}
		if (Input.GetButtonDown ("Jump")) {
			if (jumped == false) {
				firePoint.Rotate (new Vector3 (15, 0, 0));
				jumped = true;
			}
		}
		Vector3 forward = cam.transform.TransformDirection (Vector3.forward).normalized;
		forward.y = 0f;

		Vector3 right = new Vector3 (forward.z, 0f, -forward.x).normalized;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (dead) {
			myCollider.enabled = false;
			rigidBody.useGravity = false;
			foreach (Rigidbody rb in rigidBodies) {
				rb.isKinematic = false;
			}

			SetAllChildCollidersTrigger (false);
			anim.enabled = false;
			moveDirection = Vector3.zero;

		} else {
			// Move
			moveDirection = (h * right + v * forward);
			transform.position += Vector3.ClampMagnitude (moveDirection * Time.deltaTime * moveSpeed, moveSpeed) ;

			// Jumping
			bool isAerial = !IsGrounded();
			if (Input.GetButtonDown ("Jump") && !isAerial) {
				rigidBody.velocity = new Vector3(0, jumpSpeed, 0);
				isAerial = true;
			}

			// Animations
			if (!isAerial) {
				anim.SetBool ("walking", (h != 0f || v != 0f));
				anim.SetBool ("running", (Mathf.Abs (h) >= 0.7f || Mathf.Abs (v) >= 0.7f));
			}

			anim.SetBool ("jumping",   isAerial);

			if (moveDirection != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation (moveDirection);
			}

		}
	}

	void SetAllChildCollidersTrigger(bool t) {
		foreach (Collider c in GetComponentsInChildren<Collider>()) {
			if (c != myCollider)
				c.isTrigger = t;
		}
	}

	bool IsGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, 0.1f);
	}
}


