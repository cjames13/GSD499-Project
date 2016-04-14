using UnityEngine;
using System.Collections;

public class EnemyStates : MonoBehaviour, StateController {

	void StateController.TakeDamage() {
		// Damage taken animation here
	}

	void StateController.Die() {
		Destroy (gameObject);
	}
}
