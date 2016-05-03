using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 4f;
	public float jumpSpeed = 5f;
	public float rollSpeed = 4f;
	public float horizontalPenality = 0.5f;
	public bool alive = true;

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
	}
	void Update() {
		// Attacking
		bool attacking = Input.GetButton ("Fire1") || Input.GetAxis("XBox360_Triggers") < 0f;
		Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();

		if (attacking) {
			currentWeapon.Attack ();
		}

		currentWeapon.PlayAnimation(playerStates, attacking);
		// Jumping

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		isRolling = anim.GetCurrentAnimatorStateInfo (0).IsName ("Roll");

		if (Input.GetButtonDown ("Jump")) {
			rigidBody.velocity = new Vector3 (0, jumpSpeed, 0);
			playerAudio.PlayOneShot (playerJump, 1.2f);
		} else if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded () && (h != 0 || v != 0) && !isRolling) {
			rigidBody.AddRelativeForce (new Vector3 (h * rollSpeed, rollSpeed, v * rollSpeed), ForceMode.VelocityChange);
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
			float finalMoveSpeed = moveSpeed * ((Mathf.Abs (h) > 0 || v < 0) ? horizontalPenality : 1);
			transform.position += Vector3.ClampMagnitude (moveDirection * Time.deltaTime * finalMoveSpeed, finalMoveSpeed);

			if (isRolling) {
				transform.rotation = Quaternion.LookRotation (moveDirection);
			} else {
				transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
			}

			/*if (!isRolling)
				transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
			else {
				if(h > 0)
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(right), Time.deltaTime * jumpSpeed);
				else
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(-right), Time.deltaTime * jumpSpeed);
					
			}*/

			// Animations
			if (!isAerial) {
					anim.SetFloat ("HorizontalVelocity", h);
					anim.SetFloat ("VerticalVelocity", v);
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
