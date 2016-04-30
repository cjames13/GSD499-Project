using UnityEngine;
using System.Collections;

public class ResourceCrate : MonoBehaviour {
	private bool isCollected = false;
	private GameController gameController;
    private Animation anim;
    private AudioSource crateOpen;

	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
        anim = GetComponent<Animation>();
        crateOpen = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !isCollected) {
            anim.Play();
			isCollected = true;
            crateOpen.Play();
			gameController.ResourceCollected ();
			gameObject.tag = "Untagged";
		}
	}
}
