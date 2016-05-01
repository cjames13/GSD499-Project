using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayAmmoUI : MonoBehaviour {
	public Text ammoText;
	public WeaponController player;
    public RawImage gunDisplay;
    /*public RawImage rifle;
    public RawImage sword;
    public RawImage hellwailer;
    public RawImage bomb;*/

//    string currentWeapon;

	void Update () {
		int ammo = player.GetCurrentAmmo ();
        //currentWeapon = player.GetCurrentWeaponName();
        ammoText.text = ((ammo >= 0) ? ammo.ToString() : "");
		gunDisplay.texture = player.GetCurrentDisplayImage ().texture;
		//DisplayCurrentWeaponImage();
	}

    /*void DisplayCurrentWeaponImage()
    {
        if (currentWeapon == "Foam Gun")
        {
            foamGun.enabled = true;
        }
        else
        {
            foamGun.enabled = false;
        }

        if (currentWeapon == "Rifle")
        {
            rifle.enabled = true;
        }
        else
        {
            rifle.enabled = false;
        }

        if (currentWeapon == "Sword")
        {
            sword.enabled = true;
        }
        else
        {
            sword.enabled = false;
        }

        if (currentWeapon == "Hellwailer")
        {
            hellwailer.enabled = true;
        }
        else
        {
            hellwailer.enabled = false;
        }

        if (currentWeapon == "Bomb")
        {
            bomb.enabled = true;
        }
        else
        {
            bomb.enabled = false;
        }        
    }*/
}
