using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour, StateController {

	private Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void StateController.TakeDamage() {
		StartCoroutine (SetDamageLayerWeight ());
	}

	void StateController.Die() {
		// Handled by movement since we cause the rigidbodies on limbs to turn on
		// Pass the buck to movement
		GetComponent<PlayerMovement>().dead = true;
	}

	IEnumerator SetDamageLayerWeight() {
		anim.SetLayerWeight (1, 1);
		yield return new WaitForSeconds(1);
		anim.SetLayerWeight (1, 0);
	}
}
