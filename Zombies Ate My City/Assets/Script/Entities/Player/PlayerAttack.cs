using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	private Animator anim;
	private float currentWeight = 0;
	private bool draw = false;
	private Transform firingLocation;
	private WeaponController weaponController;
	private StateController playerState;

	void Start () {
		anim = GetComponent<Animator> ();
		firingLocation = GameObject.FindGameObjectWithTag ("FiringPoint").transform;
		playerState = GetComponent<StateController> ();
		weaponController = GetComponent<WeaponController> ();
	}

	void Update () {
		draw = Input.GetButton ("Fire1");

		if (draw) {
			Weapon currentWeapon = weaponController.weapons [weaponController.currentlyEquippedIndex].GetComponent<Weapon> ();
			currentWeapon.Fire (firingLocation);
		}

		playerState.RangedAttack (draw);
	}
}
