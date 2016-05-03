using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 4f;
	public float jumpSpeed = 5f;
	public float horizontalPenality = 0.5f;
	public bool alive = true;

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
	private bool isRolling = false;
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
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
		if (info.IsName ("Roll"))
			isRolling = true;
		else {
			isRolling = false;
		}
		if ((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("XBox360_A")) && IsGrounded () && h != 1 && h != -1) {
			rigidBody.velocity = new Vector3 (0, jumpSpeed, 0);
			playerAudio.PlayOneShot (playerJump, 1.2f);
		} else if ((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("XBox360_B")) && IsGrounded () && (h == 1) && info.IsName ("Locomotion")) {
			rigidBody.AddRelativeForce (new Vector3 (jumpSpeed / 1.5f, jumpSpeed / 1.5f, 0), ForceMode.VelocityChange);
			playerAudio.PlayOneShot (playerJump, 1.2f);
			anim.SetTrigger ("roll");
		} else if ((Input.GetButtonDown ("Jump") || Input.GetButtonDown ("XBox360_B")) && IsGrounded () && (h == -1) && info.IsName ("Locomotion")) {
			rigidBody.AddRelativeForce (new Vector3 (-jumpSpeed / 1.5f, jumpSpeed / 1.5f, 0), ForceMode.VelocityChange);
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


			if (!isRolling)
				transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
			else {
				if(rigidBody.velocity.x > 0)
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(right), Time.deltaTime * jumpSpeed);
				else
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(-right), Time.deltaTime * jumpSpeed);
					
			}
			// Animations
			if (!isAerial) {
					anim.SetFloat ("HorizontalVelocity", h);
					anim.SetFloat ("VerticalVelocity", v);


				/*if (anim.GetBool ("shooting") == false) {
					transform.position += Vector3.ClampMagnitude (moveDirection * Time.deltaTime * moveSpeed, moveSpeed);
					anim.SetBool ("walking", (h != 0f || v != 0f));
					anim.SetBool ("running", (Mathf.Abs (h) >= 0.7f || Mathf.Abs (v) >= 0.7f));

				} else {
					transform.position += Vector3.ClampMagnitude (moveDirection * Time.deltaTime * 1.5f, moveSpeed) ;
					anim.SetFloat ("ForwardBackward", Input.GetAxis ("Horizontal"));
					anim.SetFloat ("LeftRight", Input.GetAxis ("Vertical"));
					Debug.Log ("Horizontal" + anim.GetFloat ("LeftRight"));
					Debug.Log ("Vertical" + anim.GetFloat ("ForwardBackward"));
				}*/
			}

			anim.SetBool ("jumping",   isAerial);

			/*if (moveDirection != Vector3.zero) {
				if (!anim.GetBool ("shooting"))
					transform.rotation = Quaternion.LookRotation (moveDirection);
				else
					transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
			}*/
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
