using UnityEngine;
using System.Collections;
using Xft;
public abstract class Weapon : MonoBehaviour {
	public float attackSpeed = 0.125f;
	public Transform attackLocation;
	public bool isDefault = false;
	protected float attackTime = 0f;

	public abstract void Attack ();
	public abstract void PlayAnimation(StateController stateController, bool attacking);

}