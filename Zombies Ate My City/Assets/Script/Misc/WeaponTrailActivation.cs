using UnityEngine;
using System.Collections;
using Xft;
public class WeaponTrailActivation : MonoBehaviour {
	GameObject player;
	XWeaponTrail trail;
	Animator anim;
	virtual public void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		trail = GetComponent<XWeaponTrail> ();
		anim = player.GetComponent<Animator> ();
	}
	virtual public void Update(){
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (4);
		if (currentState.IsName ("Not Meleeing")) {
			trail.enabled = false;
		} else
			trail.enabled = true;
	}
}
