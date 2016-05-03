using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {
	public bool ragdollOnDeath = false;
	public float deathTime = 3f;

	private Animator anim;
	private EnemyController enemyController;
	private GameController gameController;
	private bool isSinking = false;
	private const float sinkSpeed = 0.5f;

	//Ragdoll
	private Rigidbody rigidBody;
	private Rigidbody[] rigidBodies;
	private CapsuleCollider myCollider;
	private List<Collider> colliders;
	private SphereCollider sphereCollider;

	void Start(){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		anim = GetComponent<Animator> ();
		enemyController = GetComponent<EnemyController> ();
		anim.SetLayerWeight (1, 1f);
		anim.SetLayerWeight (2, 1f);
		if (enemyController.rangedAttacker) {
			anim.SetLayerWeight (3, 1f);
		}

		sphereCollider = GetComponent<SphereCollider> ();
		rigidBody = GetComponent<Rigidbody> ();
		rigidBodies = GetComponentsInChildren<Rigidbody> ();
		myCollider = GetComponent<CapsuleCollider> ();
		rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

		//Ragdoll
		if (ragdollOnDeath) {
			foreach (Rigidbody rb in rigidBodies) {
				if (rb != rigidBody)
					rb.isKinematic = true;
			}
			SetAllChildCollidersTrigger (true);
		}
	}

	//ragdoll
	void SetAllChildCollidersTrigger(bool t) {
		foreach (Collider c in GetComponentsInChildren<Collider>()) {
			if (c != myCollider)
				c.isTrigger = t;
		}
	}

	void Update(){
		if (isSinking) {
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	void StateController.TakeDamage() {
		// Damage taken animation here

		if (anim.GetBool("dying") != true)
		anim.SetTrigger("hurt");
	}

	void StateController.Die() {
		if (sphereCollider) {
			sphereCollider.enabled = false;
		}

		if (ragdollOnDeath) {
			myCollider.enabled = false;
			rigidBody.useGravity = false;
			foreach (Rigidbody rb in rigidBodies) {
				rb.isKinematic = false;
			}

			SetAllChildCollidersTrigger (false);
			anim.enabled = false;
		} else {
			rigidBody.useGravity = false;
			anim.SetBool ("dying", true);
		}
		DropItems ();
		gameController.IncreaseScore (enemyController.scoreValue);
		gameController.IncreaseKills (1);
		StartCoroutine (Burning ());
	}

	void DropItems() {
		if (enemyController.itemDrops.Length > 0 && Random.Range(0F, 1F) <= enemyController.dropChance) {
			GameObject item = enemyController.itemDrops [Random.Range (0, enemyController.itemDrops.Length - 1)];
			Instantiate (item, transform.position, item.transform.rotation);
		}
	}

	void StateController.MeleeAttack(bool attacking){
		anim.SetBool ("attacking", attacking);
	}

	void StateController.RangedAttack(bool attacking, bool isRifle){
		anim.SetTrigger ("cast");
	}

	void StateController.ThrownAttack(bool attacking) {}

	void StateController.Walk(){
		if (anim) {
			anim.SetBool ("walking", true);
		}
	}

	IEnumerator Burning()
	{
		
		yield return new WaitForSeconds (2);
		rigidBody.useGravity = false;
		if (ragdollOnDeath) {
			foreach (Rigidbody rb in rigidBodies) {
				if (rb != rigidBody)
					rb.useGravity = false;
			}
		}
		isSinking = true;
		myCollider.isTrigger = true;
		SetAllChildCollidersTrigger (true);
		transform.Find ("RingOfFire").gameObject.SetActive (true);

		yield return new WaitForSeconds (deathTime);
		Destroy (gameObject);
	}

	bool StateController.IsAnimationPlaying(string layerName, string animationName) {
		return anim.GetCurrentAnimatorStateInfo (anim.GetLayerIndex (layerName)).IsName (animationName);
	}
}