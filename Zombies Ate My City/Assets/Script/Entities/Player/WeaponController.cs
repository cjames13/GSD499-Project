using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	public GameObject[] weapons;
	public int currentlyEquippedIndex = 0;

	private GameObject weaponSlot;

	void Start () {
		weaponSlot = GameObject.FindGameObjectWithTag ("WeaponSlot");

		// Get all children
		weapons = new GameObject[weaponSlot.transform.childCount];
		for (int i = 0; i < weaponSlot.transform.childCount; i++) {
			weapons[i] = weaponSlot.transform.GetChild(i).gameObject;
			weapons [i].SetActive (false);
		}

		// TODO: Look for weapon marked as default
		weapons [0].SetActive (true);
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
}
