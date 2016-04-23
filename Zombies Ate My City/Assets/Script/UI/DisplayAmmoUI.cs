using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayAmmoUI : MonoBehaviour {
	public Text ammoText;
	public WeaponController player;

	void Update () {
		int ammo = player.GetCurrentAmmo ();
		ammoText.text = player.GetCurrentWeaponName () + ((ammo >= 0) ? ": " + ammo.ToString() : "");
	}
}
