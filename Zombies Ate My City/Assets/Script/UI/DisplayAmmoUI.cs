using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayAmmoUI : MonoBehaviour {
	public Text ammoText;
    public Text maxAmmoText;
	public WeaponController player;
    public RawImage gunDisplay;
	void Update () {
		int ammo = player.GetCurrentAmmo ();
        int maxAmmo = player.GetMaxAmmo();
        ammoText.text = " " + ((ammo >= 0) ? ammo.ToString() : "");
		maxAmmoText.text = (maxAmmo > -1) ? "/  " + maxAmmo : "";
		gunDisplay.texture = player.GetCurrentDisplayImage ().texture;
	}
}
