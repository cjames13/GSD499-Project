using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHealth;
	public int currentHealth;
	public float invulnTimeAfterHit = 0f;

	private StateController animController;
	private float lastHitTime;

	// Use this for initialization
	void Start () {
		animController = GetComponent<StateController> ();
		currentHealth = maxHealth;
		lastHitTime = Time.time;
	}

	void Update() {
		if (currentHealth <= 0) {
			animController.Die ();
		}
	}

	public void Damage(int d) {
		if (Time.time - lastHitTime >= invulnTimeAfterHit) {
			currentHealth -= d;
			animController.TakeDamage ();
			lastHitTime = Time.time;
		}
	}
}
