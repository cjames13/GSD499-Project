using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceCrate : MonoBehaviour {
	public float healAmount = 2f;
    public Text collectedText;
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
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.Heal(healAmount);
            }
			gameController.ResourceCollected ();
			gameObject.tag = "Untagged";
		}
        else if (other.tag == "Player" && isCollected && !gameController.allResourcesCollected) {
            collectedText.text = "This resource crate has already been collected.";
        }
        else if (other.tag == "Player" && isCollected && gameController.allResourcesCollected){
            collectedText.text = "All resources have been collected.\nFind a way out of here before it's too late!";
        }
	}

    void OnTriggerExit (Collider other) {
        if (other.tag == "Player") {
            isCollected = true;
            collectedText.text = "";
        }
    }
}
