using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 4f;
	public float jumpSpeed = 5f;
	public float rollSpeed = 4f;
	public float horizontalPenalty = 0.5f;
	public bool alive = true;

	// Attacking
	bool attacking = false;

	// Rolling
	private bool isRolling = false;
	private bool isJumping = false;

	// Animation
	private Animator anim;
	private Rigidbody rigidBody;
	private Rigidbody[] rigidBodies;
	private CapsuleCollider myCollider;
	private List<Collider> colliders;

	private GameObject lightObject;
	private Light pointLight;
	private Camera cam;
	private ControlContext controls;

	// Controllers
	private StateController playerStates;
	private WeaponController weaponController;

	// Sounds
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
		controls = new ControlContext (new OverheadControl ());
	}
	void Update() {
		// Attacking
		attacking = Input.GetButton ("Fire1") || Input.GetAxis("XBox360_Triggers") < 0f;
		Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();

		if (attacking) {
			currentWeapon.Attack ();
		}

		currentWeapon.PlayAnimation(playerStates, attacking);
		// Jumping

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		isRolling = anim.GetCurrentAnimatorStateInfo (0).IsName ("Roll");
		isJumping = anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump");
		if (Input.GetButtonDown ("Jump") && IsGrounded()) {
			rigidBody.velocity = new Vector3 (0, jumpSpeed, 0);
			playerAudio.PlayOneShot (playerJump, 1.2f);
		} else if (Input.GetButtonDown("Roll") && IsGrounded () && (h != 0 || v != 0) && !isRolling && !isJumping) {
			rigidBody.AddRelativeForce (new Vector3 (h * rollSpeed, jumpSpeed / 2f, v * rollSpeed), ForceMode.VelocityChange);
			playerAudio.PlayOneShot (playerJump, 1.2f);
			anim.SetTrigger ("roll");
		}

	}

	void FixedUpdate() {
		// Movement direction
		Vector3 forward = cam.transform.TransformDirection (Vector3.forward).normalized;
		forward.y = 0f;

		Vector3 right = new Vector3 (forward.z, 0f, -forward.x).normalized;

		Vector3 moveDirection = Vector3.zero;

		if(alive) {
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");

			// Move
			moveDirection = (h * right + v * forward);
			bool isAerial = !IsGrounded();

			transform.position += controls.SetPlayerMovement (h, v, moveDirection, moveSpeed, horizontalPenalty);
			transform.rotation = controls.SetPlayerRotation (cam, moveDirection, isRolling, attacking);

			/*if (isRolling && moveDirection != Vector3.zero) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * jumpSpeed);
			} else {
				transform.rotation = controls.SetPlayerRotation (cam, moveDirection, isRolling);
				transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
			}*/
				
			// Animations
			if (!isAerial) {
				controls.SetPlayerMovementAnimation (anim, h, v, attacking);
			}

			anim.SetBool ("jumping",   isAerial);
		}
	}

	// Is the player airborne?
	bool IsGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, 0.1f);
	}
    void PlayerLeftFootStep()
    {
        if (!playerAudio.isPlaying)
            playerAudio.PlayOneShot(playerLeftStep, 0.8f);
    }

    void PlayerRightFootStep()
    {
        if (!playerAudio.isPlaying)
            playerAudio.PlayOneShot(playerRightStep, 0.8f);
    }
}
