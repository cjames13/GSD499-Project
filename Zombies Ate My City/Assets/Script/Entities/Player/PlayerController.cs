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
	public bool attacking = false;

	// Rolling
	private bool isRolling = false;

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
	private GameObject leftFoot;
	private GameObject rightFoot;

	void Awake(){
		rigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		cam =  GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		playerStates = GetComponent<StateController> ();
		weaponController = GetComponent<WeaponController> ();
        playerAudio = GetComponent<AudioSource>();
		controls = PlayerSettingsSingleton.Instance.controlContext;
		leftFoot = GameObject.Find ("LeftFootLocation");
		rightFoot = GameObject.Find ("RightFootLocation");

	}
	void Update() {

		// Attacking
		attacking = Input.GetButton ("Fire1") || Input.GetAxis("XBox360_Triggers") < 0f;
		Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();


		// Jumping

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (attacking) {
			currentWeapon.Attack ();
		}

		currentWeapon.PlayAnimation(playerStates, attacking);

		isRolling = anim.GetCurrentAnimatorStateInfo (0).IsName ("Roll");
		bool isJumping = anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump");

		if (Input.GetButtonDown ("Jump") && IsGrounded()) {
			rigidBody.velocity = new Vector3 (0, jumpSpeed, 0);
			playerAudio.PlayOneShot (playerJump, 1.2f);
		} else if (Input.GetButtonDown("Roll") && IsGrounded () && (h != 0 || v != 0) && !isRolling && !isJumping) {
			controls.PerformRoll (h, v, rigidBody, jumpSpeed, rollSpeed);
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

			transform.position += controls.SetPlayerMovement (h, v, moveDirection, moveSpeed, horizontalPenalty, attacking);
			transform.rotation = controls.SetPlayerRotation (cam, moveDirection, isRolling);

			// Animations
			if (!isAerial) {
				controls.SetPlayerMovementAnimation (anim, h, v, attacking);
			}

			anim.SetBool ("jumping",isAerial);
		}
	}

	// Is the player airborne?
	bool IsGrounded(){
		if (Physics.Raycast (leftFoot.transform.position, -Vector3.up, 0.1f) == true ||
		    Physics.Raycast (rightFoot.transform.position, -Vector3.up, 0.1f) == true)
			return true;
		else
			return false;
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
