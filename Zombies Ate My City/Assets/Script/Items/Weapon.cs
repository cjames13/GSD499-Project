using UnityEngine;
using System.Collections;
public abstract class Weapon : MonoBehaviour {
	public float attackSpeed = 0.125f;
	public bool isDefault = false;

	protected float attackTime = 0f;
	protected Transform attackLocation;

	void Start() {
		attackLocation = GameObject.FindGameObjectWithTag ("AttackLocation").transform;
	}

	public abstract void Attack ();
	public abstract void PlayAnimation(StateController stateController, bool attacking);

}