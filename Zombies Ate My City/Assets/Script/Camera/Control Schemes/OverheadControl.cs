using UnityEngine;
using System.Collections;

public class OverheadControl : ControlStrategy {
	Vector3 lastMoveDirection = Vector3.zero;
	// This sets the player's movement. Whatever value is returned from this function will be added to the player's current position
	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, float moveSpeed, float movementPenalty) {
		return Vector3.ClampMagnitude (moveDirection,moveSpeed) * Time.deltaTime * moveSpeed;
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling, bool isShooting) {
		if (!isShooting && moveDirection != Vector3.zero) {
			lastMoveDirection = moveDirection;
			return Quaternion.LookRotation (moveDirection);
		} 
		else {
			return Quaternion.LookRotation(lastMoveDirection);
		}
	}
	// The animation for the player's movement goes here
	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
		if (!isShooting)
			animator.SetFloat ("VerticalVelocity", Mathf.Abs (horizontalInput) + Mathf.Abs (verticalInput));
		else {
			animator.SetFloat ("HorizontalVelocity", horizontalInput);
			animator.SetFloat ("VerticalVelocity", verticalInput);
		}
	}
}