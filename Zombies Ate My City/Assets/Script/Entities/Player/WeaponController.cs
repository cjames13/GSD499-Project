﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {
	public GameObject[] weapons;
	public int currentlyEquippedIndex = 0;
	private GameObject weaponSlot;

    public AudioClip weaponChange;
    public AudioClip swordEquip;
    public AudioClip bombEquip;
    private AudioSource weaponAudio;

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

        weaponAudio = GetComponent<AudioSource>();
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
		weapons [currentlyEquippedIndex].GetComponent<Weapon> ().PlayAnimation (GetComponent<StateController> (), false);
		weapons [currentlyEquippedIndex].SetActive (false);
		currentlyEquippedIndex = (currentlyEquippedIndex + direction + weapons.Length) % weapons.Length;
		weapons [currentlyEquippedIndex].SetActive (true);

        if (GetCurrentWeaponName() == "Sword")
        {
            weaponAudio.PlayOneShot(swordEquip, 0.6f);
        }
        else if (GetCurrentWeaponName() == "Bomb")
        {
            weaponAudio.PlayOneShot(bombEquip, 0.6f);
        }
        else
        {
            weaponAudio.PlayOneShot(weaponChange, 0.8f);
        }
	}

	public int GetCurrentAmmo() {
		RangedWeapon currentGun = weapons [currentlyEquippedIndex].GetComponent<RangedWeapon> ();
		return (currentGun != null) ? currentGun.currentAmmo : -1;
	}

    public int GetMaxAmmo()
    {
        RangedWeapon currentGun = weapons[currentlyEquippedIndex].GetComponent<RangedWeapon>();
        return (currentGun != null) ? currentGun.maxAmmo : -1;
    }

    public string GetCurrentWeaponName() {
		return weapons [currentlyEquippedIndex].name;
	}

	public RawImage GetCurrentDisplayImage() {
		return weapons [currentlyEquippedIndex].GetComponent<Weapon>().displayImage;
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