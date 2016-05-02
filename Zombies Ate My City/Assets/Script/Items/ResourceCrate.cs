using UnityEngine;
using System.Collections;

public class ResourceCrate : MonoBehaviour {
	private bool isCollected = false;
	private GameController gameController;
    private Animation anim;
    private AudioSource crateOpen;
    private Health playerHealth;
    private bool healthGain = false;
    private bool waitOver = false;
    float speed = 2.0f;

    public AudioClip healthGainClip;

	void Start() {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
        anim = GetComponent<Animation>();
        crateOpen = GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}

    void FixedUpdate() {
        if (healthGain && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            StartCoroutine(GainHealth());
        }
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !isCollected) {
            anim.Play();
            crateOpen.Play();
            isCollected = true;
            healthGain = true;        
			gameController.ResourceCollected ();
			gameObject.tag = "Untagged";
		}
	}

    IEnumerator GainHealth() {
        yield return new WaitForSeconds(3);
        crateOpen.PlayOneShot(healthGainClip);
        playerHealth.currentHealth = (int)Mathf.Lerp(playerHealth.currentHealth, playerHealth.maxHealth, Time.deltaTime * speed);
        healthGain = false;
    }
}
