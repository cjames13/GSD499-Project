using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Weapon : MonoBehaviour {
	public float attackSpeed = 0.125f;
	public bool isDefault = false;
	public RawImage displayImage;

	protected float attackTime = 0f;
	protected Transform attackLocation;

	void Start() {
		attackLocation = GameObject.FindGameObjectWithTag ("AttackLocation").transform;
	}

	void Update() {
		attackTime -= Time.deltaTime;
	}

	protected bool OnCooldown() {
		return attackTime > 0f;
	}

	public abstract void Attack ();
	public abstract void PlayAnimation(StateController stateController, bool attacking);

}