using UnityEngine;
using System.Collections;

public class Movement: MonoBehaviour {
	//Variables
	public float speed = 2f;
	public float jumpSpeed = 5; 
	private Vector3 moveDirection;
	Animator anim;
	Rigidbody[] rigidBodies;
	CapsuleCollider[] capsuleColliders;
	CapsuleCollider capsuleCollider;
	SphereCollider[] sphereColliders;
	BoxCollider[] boxColliders;
	Rigidbody rigidBody;
	bool dead = false;
	GameObject cameraObject;
	Camera cam;
	float distanceToGround = 0.1f;
	bool jumping = false;

	void Awake(){
		dead = false;
		rigidBody = GetComponent<Rigidbody>();
		rigidBodies = GetComponentsInChildren<Rigidbody> ();
		capsuleColliders = GetComponentsInChildren <CapsuleCollider> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		sphereColliders = GetComponentsInChildren<SphereCollider> ();
		boxColliders = GetComponentsInChildren<BoxCollider> ();
		foreach (Rigidbody rb in rigidBodies) {
			if (rb != rigidBody)
				rb.isKinematic = true;
//			else
//				rb.useGravity = false;
		}
		rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		foreach (SphereCollider sc in sphereColliders)
			sc.isTrigger = true;
		foreach (BoxCollider bc in boxColliders)
			bc.isTrigger = true;
		foreach (CapsuleCollider cc in capsuleColliders) {
			if (cc != capsuleCollider)
				cc.isTrigger = true;
		}
		anim = GetComponent<Animator> ();
		anim.enabled = true;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		cam = cameraObject.GetComponent<Camera> ();
	}

	bool IsGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, distanceToGround);
	}
	void FixedUpdate() {
		Vector3 forward = cam.transform.TransformDirection (Vector3.forward);
		forward.y = 0f;
		forward.Normalize ();
		Vector3 right = new Vector3 (forward.z, 0f, -forward.x);
		right.Normalize ();
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (dead) {
			capsuleCollider.enabled = false;
			rigidBody.useGravity = false;
			foreach (Rigidbody rb in rigidBodies) {
				rb.isKinematic = false;
			}
			foreach (CapsuleCollider cc in capsuleColliders) {
				cc.isTrigger = false;
			}
			foreach (BoxCollider bc in boxColliders)
				bc.isTrigger = false;
			foreach (SphereCollider sc in sphereColliders)
				sc.isTrigger = false;

			anim.enabled = false;
		}

		if (!dead) {
			moveDirection = (h * right + v * forward);
			transform.position += Vector3.ClampMagnitude (moveDirection, speed) * Time.deltaTime * speed;
		}
		else
			moveDirection = Vector3.zero;

		if ((h != 0f || v != 0f) && anim.GetBool("jumping") == false) {
			{
				if ((Mathf.Abs(h) < 0.7f && Mathf.Abs(v) < 0.7f) && (Mathf.Abs(h) > -0.7f && Mathf.Abs(v) > -0.7f)) {
					speed = 2f;
					anim.SetBool ("walking", true);
				}
				else {
					speed = 4f;
					anim.SetBool ("walking", true);
					anim.SetBool ("running", true);
				}
			}
		} else if (h == 0f && v == 0f){
			speed = 2f;
			anim.SetBool ("walking", false);
			anim.SetBool ("running", false);
		}

		if (Input.GetButtonDown ("Jump") && IsGrounded ()) {
			jumping = true;
			rigidBody.velocity = new Vector3(0, jumpSpeed, 0);
		} 
		if (!IsGrounded ()) {
			anim.SetBool ("jumping", true);
			jumping = true;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, -Vector3.up, out hit)){
				float groundDistance = hit.distance;
				if (groundDistance > 0.5f)
					jumping = false;
				if (groundDistance < 0.5f && jumping == false)
					anim.SetBool ("jumping", false);
			}
		} else {
			anim.SetBool ("jumping", false);
		}
		if (moveDirection != Vector3.zero)
			transform.rotation = Quaternion.LookRotation (moveDirection);


	
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Fire" || other.transform.tag == "Enemy")
			dead = true;
	}
}