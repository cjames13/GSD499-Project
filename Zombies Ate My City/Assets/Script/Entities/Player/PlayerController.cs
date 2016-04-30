using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 4f;
	public float jumpSpeed = 5f;
	public bool alive = true;
//	bool attack = false;

	Animator anim;
	Rigidbody rigidBody;
	Rigidbody[] rigidBodies;
	CapsuleCollider myCollider;
	List<Collider> colliders;
	private GameObject lightObject;
	private Light pointLight;
	Camera cam;

	private StateController playerStates;
	private WeaponController weaponController;

    public AudioClip playerJump;
    public AudioClip playerLeftStep;
    public AudioClip playerRightStep;
    private AudioSource playerAudio;

	void Awake(){
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		cam =  GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		playerStates = GetComponent<StateController> ();
		weaponController = GetComponent<WeaponController> ();
        playerAudio = GetComponent<AudioSource>();
	}

	void Update() {
		// Attacking
		bool attacking = Input.GetButton ("Fire1");
		Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();

		if (attacking) {
			currentWeapon.Attack ();
		}

		currentWeapon.PlayAnimation(playerStates, attacking);

		// Jumping
		if (Input.GetButtonDown ("Jump") && IsGrounded()) {
			rigidBody.velocity = new Vector3(0, jumpSpeed, 0);
            playerAudio.PlayOneShot(playerJump, 1.2f);
		}
	}

	void FixedUpdate() {
		// Movement direction
		Vector3 forward = cam.transform.TransformDirection (Vector3.forward).normalized;
		forward.y = 0f;

		Vector3 right = new Vector3 (forward.z, 0f, -forward.x).normalized;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Vector3 moveDirection = Vector3.zero;

		if(alive) {
			// Move
			moveDirection = (h * right + v * forward);
			transform.position += Vector3.ClampMagnitude (moveDirection * Time.deltaTime * moveSpeed, moveSpeed) ;

			bool isAerial = !IsGrounded();

			// Animations
			if (!isAerial) {
				if (anim.GetBool ("shooting") == false) {
					anim.SetBool ("walking", (h != 0f || v != 0f));
					anim.SetBool ("running", (Mathf.Abs (h) >= 0.7f || Mathf.Abs (v) >= 0.7f));

				} else {
					
					anim.SetFloat ("ForwardBackward", Input.GetAxis ("Horizontal"));
					anim.SetFloat ("LeftRight", Input.GetAxis ("Vertical"));
					Debug.Log ("Horizontal" + anim.GetFloat ("LeftRight"));
					Debug.Log ("Vertical" + anim.GetFloat ("ForwardBackward"));
				}
			}

			anim.SetBool ("jumping",   isAerial);

			if (moveDirection != Vector3.zero) {
				if (anim.GetBool ("shooting") != true)
					transform.rotation = Quaternion.LookRotation (moveDirection);
				else
					transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
			}
		}
	}

	// Is the player airborne?
	bool IsGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, 0.1f);
	}

    void PlayerLeftFootStep()
    {
        playerAudio.PlayOneShot(playerLeftStep, 0.8f);
    }

    void PlayerRightFootStep()
    {
        playerAudio.PlayOneShot(playerRightStep, 0.8f);
    }
}
