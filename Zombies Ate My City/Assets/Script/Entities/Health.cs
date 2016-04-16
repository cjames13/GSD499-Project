using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHealth;
	public int currentHealth;

	private StateController animController;

	// Use this for initialization
	void Start () {
		animController = GetComponent<StateController> ();
		currentHealth = maxHealth;
	}

	void Update() {
		if (currentHealth <= 0) {
			animController.Die ();
		}
	}

	public void Damage(int d) {
		currentHealth -= d;
		animController.TakeDamage ();
	}
}
