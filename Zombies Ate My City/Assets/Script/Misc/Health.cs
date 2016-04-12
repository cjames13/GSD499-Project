using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHealth;
	public int currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}

	public void Damage(int d) {
		currentHealth -= d;
		if (currentHealth <= 0)
			Debug.Log ("DEAD");
	}
}
