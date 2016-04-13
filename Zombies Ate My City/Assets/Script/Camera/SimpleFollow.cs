using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {
	public Transform target;
	public float distance;
	GameObject player;
	PlayerMovement playerMovementScript;
	// Update is called once per frame
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovementScript = player.GetComponent<PlayerMovement> ();
	}
	void FixedUpdate () {
		
		transform.LookAt (target);
		transform.position = new Vector3(target.transform.position.x, 
			target.transform.position.y + distance / 1.25f, target.transform.position.z - distance);
	}
}
