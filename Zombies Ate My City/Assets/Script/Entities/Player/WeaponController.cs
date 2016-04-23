using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	public GameObject[] weapons;
	public int currentlyEquippedIndex = 0;
	private GameObject weaponSlot;

	void Awake () {
		weaponSlot = GameObject.FindGameObjectWithTag ("WeaponSlot");

		// Get all children
		weapons = new GameObject[weaponSlot.transform.childCount];
		int defaultWeaponIndex = 0;
		for (int i = 0; i < weaponSlot.transform.childCount; i++) {
			weapons[i] = weaponSlot.transform.GetChild(i).gameObject;
			weapons [i].SetActive (false);
			if (weapons [i].GetComponent<Weapon> ().isDefault)
				defaultWeaponIndex = i;
		}

		currentlyEquippedIndex = defaultWeaponIndex;
		weapons [defaultWeaponIndex].SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Weapon Switch L")) {
			SwitchWeapon (-1);
		} else if(Input.GetButtonDown("Weapon Switch R")) {
			SwitchWeapon (1);
		}
	}

	// Direction is either -1 or 1. -1 is left, 1 is right.
	void SwitchWeapon(int direction) {
		weapons [currentlyEquippedIndex].SetActive (false);
		currentlyEquippedIndex = (currentlyEquippedIndex + direction + weapons.Length) % weapons.Length;
		weapons [currentlyEquippedIndex].SetActive (true);
	}

	public int GetCurrentAmmo() {
		RangedWeapon currentGun = weapons [currentlyEquippedIndex].GetComponent<RangedWeapon> ();
		return (currentGun != null) ? currentGun.currentAmmo : -1;
	}

	public string GetCurrentWeaponName() {
		return weapons [currentlyEquippedIndex].name;
	}

	public void IncreaseAmmo(string gunName, int amount) {
		foreach (GameObject gun in weapons) {
			RangedWeapon gunScript = gun.GetComponent<RangedWeapon> ();
			if (gun.name == gunName && gunScript != null) {
				gun.GetComponent<RangedWeapon> ().IncreaseAmmo (amount);
				break;
			}
		}
	}
}