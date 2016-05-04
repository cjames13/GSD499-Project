using UnityEngine;
using System.Collections;

public class OverheadControl : ControlStrategy {
	// This sets the player's movement. Whatever value is returned from this function will be added to the player's current position
	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, float moveSpeed, float movementPenalty) {
		return Vector3.ClampMagnitude (moveDirection,moveSpeed) * Time.deltaTime * moveSpeed;
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling) {
		return Quaternion.LookRotation (moveDirection);
	}

	// The animation for the player's movement goes here
	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
		animator.SetFloat ("VerticalVelocity", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
	}
}