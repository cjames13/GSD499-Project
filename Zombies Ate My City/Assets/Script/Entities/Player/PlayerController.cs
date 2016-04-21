using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 4f;
	public float jumpSpeed = 5f;
	public bool dead = false;
	public bool swing = false;
	Vector3 moveDirection;
	Animator anim;
	Rigidbody rigidBody;
	Rigidbody[] rigidBodies;

	CapsuleCollider myCollider;
	List<Collider> colliders;

	//bool jumped = false;
	Camera cam;

	private StateController playerStates;
	private WeaponController weaponController;
	private Transform firingLocation;

	void Awake(){
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		cam =  GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		playerStates = GetComponent<StateController> ();
		weaponController = GetComponent<WeaponController> ();
		firingLocation = GameObject.FindGameObjectWithTag ("FiringPoint").transform;
	}
	void Update()//So GetButtonDown will always activate
	{
		if (Input.GetButtonDown ("Fire1") && weaponController.weapons [weaponController.currentlyEquippedIndex].name == "Bomb") 
			swing = true;
		if (swing) {
			Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();
			currentWeapon.ThrowOrHit (firingLocation);
		}
	}
	void FixedUpdate() {
		bool draw = Input.GetButton ("Fire1");

		if (draw) {
			Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();
			if (currentWeapon.tag != "CloseRange")
				currentWeapon.Fire (firingLocation);
		}

		playerStates.RangedAttack (draw);

		Vector3 forward = cam.transform.TransformDirection (Vector3.forward).normalized;
		forward.y = 0f;

		Vector3 right = new Vector3 (forward.z, 0f, -forward.x).normalized;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (dead) {
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

	bool IsGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, 0.1f);
	}
}
