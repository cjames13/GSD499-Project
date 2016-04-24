using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyStates : MonoBehaviour, StateController {
	public GameObject rangedAttackObject;

	private Animator anim;
	private EnemyController enemyController;
	private GameController gameController;

	private bool playing = true;
	private bool shooting = true;

	public float magicAttackDelay = 7f;
	private float magicAttackTime;

	void Start(){
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		anim = GetComponent<Animator> ();
		magicAttackTime = magicAttackDelay;
		enemyController = GetComponent<EnemyController> ();
	}
	void StateController.TakeDamage() {
		// Damage taken animation here
		StartCoroutine (SetDamageLayerWeight ());
	}

	void StateController.Die() {
		StartCoroutine (Dying ());
	}
	IEnumerator SetDamageLayerWeight() {
		if (anim)
		anim.SetLayerWeight (1, 1);
		yield return new WaitForSeconds(1);
		if (anim)
		anim.SetLayerWeight (1, 0);
	}
	void StateController.MeleeAttack(bool attacking){
		if (anim) {
			if (anim.GetLayerWeight (1) == 1)
				attacking = false;
			if (attacking) {
				anim.SetLayerWeight (2, 1);
				if (anim.layerCount > 3)
					anim.SetLayerWeight (3, 0);
			} else {
				anim.SetLayerWeight (2, 0);
			}
		}
	}

	void StateController.RangedAttack(bool attacking){
		if (anim) {
			if (anim.GetLayerWeight (1) == 1)
				attacking = false;
			if (attacking) {
				magicAttackTime -= Time.deltaTime;
				if (magicAttackTime <= 2) {
					AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (3);
					float playbackTime = currentState.normalizedTime % 1;
					if (playbackTime > 0.9f)
						playing = false;
					if (playing == true) {
						if (shooting == true) {
							Instantiate (rangedAttackObject,
								new Vector3 (transform.position.x, transform.position.y + 0.5f,
									transform.position.z - 1), transform.rotation);
							shooting = false;
						}
						anim.SetLayerWeight (3, 1);
					} else {
						anim.SetLayerWeight (3, 0);
					}
					if (magicAttackTime <= 0) {
						playing = true;
						shooting = true;
						magicAttackTime = magicAttackDelay;
					}
				} else {
					anim.SetLayerWeight (3, 0);
				}
			} else {
				anim.SetLayerWeight (3, 0);
			}
		}
	}

	void StateController.ThrownAttack(bool attacking) {}

	void StateController.Walk(){
		if (anim)
		anim.SetBool ("walking", true);
	}
	IEnumerator Dying()
	{
		anim.SetBool ("dying", true);
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
		gameController.IncreaseScore (enemyController.scoreValue);
	}
}