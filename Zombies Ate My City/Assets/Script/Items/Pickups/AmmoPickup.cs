using UnityEngine;
public class AmmoPickup : MonoBehaviour {
	public string gunName;
	public int amount;
    private AudioSource ammoAudio;

    void Start(){
        ammoAudio = GetComponent<AudioSource>();
    }

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<WeaponController>().IncreaseAmmo (gunName, amount);
            ammoAudio.Play();
			//Destroy (gameObject);
		}
	}
}