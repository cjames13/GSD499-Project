using UnityEngine;
using System.Collections;

public class Resources : MonoBehaviour {
	private bool isCollected = false;
	private GameController gameController;

	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !isCollected) {
			isCollected = true;
			gameController.ResourceCollected ();
		}
	}
}
