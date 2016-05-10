using UnityEngine;
public class HealthPickup : MonoBehaviour {
	public int amount;
    private AudioSource healthAudio;

    void Start() {
        healthAudio = GetComponent<AudioSource>();
    }

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<Health>().currentHealth += amount;
            healthAudio.Play();
			Destroy (gameObject);
		}
	}
}