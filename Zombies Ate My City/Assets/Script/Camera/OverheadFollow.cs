using UnityEngine;
using System.Collections;

public class OverheadFollow : MonoBehaviour {
	public Transform target;
	public float yDistance, zDistance;
    public float movementDamping = 5.0f;
	public float rotationDamping = 6.0f;
    
	private Color color;
    private float alpha = 1.0f;

    void LateUpdate() {
		Vector3 wantedPosition = target.TransformPoint (0, yDistance, -zDistance);
		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * movementDamping);

		Quaternion rotate  = Quaternion.LookRotation(target.position - transform.position, target.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * rotationDamping);
     }
 }