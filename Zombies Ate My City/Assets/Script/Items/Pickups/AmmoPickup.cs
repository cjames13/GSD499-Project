using UnityEngine;
public class AmmoPickup : MonoBehaviour {
	public string gunName;
	public int amount;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<WeaponController>().IncreaseAmmo (gunName, amount);
			Destroy (gameObject);
		}
	}
}