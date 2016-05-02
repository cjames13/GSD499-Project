using UnityEngine;
using System.Collections;

public class ResourceCrate : MonoBehaviour {
	public float healAmount = 2f;
	private bool isCollected = false;
	private GameController gameController;
    private Animation anim;
    private AudioSource crateOpen;
    private Health playerHealth;


	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
        anim = GetComponent<Animation>();
        crateOpen = GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !isCollected) {
            anim.Play();
            crateOpen.Play();
            isCollected = true;
			playerHealth.Heal (healAmount);
			gameController.ResourceCollected ();
			gameObject.tag = "Untagged";
		}
	}
}
