using UnityEngine;
using System.Collections;
using Xft;

public class WeaponTrailActivation : MonoBehaviour {
	private Animator anim;
	private XWeaponTrail trail;

	virtual public void Start(){
		trail = GetComponent<XWeaponTrail> ();
		anim = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
	}
	virtual public void Update(){
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (4);
		if (currentState.IsName ("Not Meleeing")) {
			trail.StopSmoothly (1);
			trail.Activate ();
		} 
	}
}
