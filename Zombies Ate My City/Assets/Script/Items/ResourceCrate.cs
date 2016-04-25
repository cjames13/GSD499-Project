using UnityEngine;
using System.Collections;

public class ResourceCrate : MonoBehaviour {
	private bool isCollected = false;
	private GameController gameController;
    private Animation anim;

	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
        anim = GetComponent<Animation>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !isCollected) {
            anim.Play();
			isCollected = true;
			gameController.ResourceCollected ();
			gameObject.tag = "Untagged";
		}
	}
}
