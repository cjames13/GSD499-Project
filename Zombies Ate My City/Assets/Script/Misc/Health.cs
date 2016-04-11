using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int maxHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(int d) {
		maxHealth -= d;
		if (maxHealth <= 0)
			Debug.Log ("DEAD");
	}
}
