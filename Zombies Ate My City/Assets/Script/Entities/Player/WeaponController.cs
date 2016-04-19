using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	public GameObject[] weapons;
	public int currentlyEquippedIndex = 0;
	GameObject player;
	Animator anim;
	bool rotated = false;
	private GameObject weaponSlot;

	void Start () {
		weaponSlot = GameObject.FindGameObjectWithTag ("WeaponSlot");
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator> ();
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
		CurrentAnimation ();
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
	void CurrentAnimation()
	{
		if (weapons [currentlyEquippedIndex].name == "Rifle") {
			if (anim.GetLayerWeight (4) != 1) {
				anim.SetLayerWeight (3, 1);
			} else 
				anim.SetLayerWeight (4, 1);
		} 
		else {
			anim.SetLayerWeight (3, 0);
			anim.SetLayerWeight (4, 0);
		}
	}
}
