using UnityEngine;
using System.Collections;

public class OverheadControl : ControlStrategy {
	Vector3 lastMoveDirection = Vector3.zero;
	// This sets the player's movement. Whatever value is returned from this function will be added to the player's current position
	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, 
		float moveSpeed, float movementPenalty, bool isShooting) {
		return Vector3.ClampMagnitude (moveDirection, moveSpeed).normalized * Time.deltaTime * moveSpeed;
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling) {
		if (moveDirection != Vector3.zero) {
			lastMoveDirection = moveDirection;
			return Quaternion.LookRotation (moveDirection);
		} 
		else if (lastMoveDirection != Vector3.zero) {
			return Quaternion.LookRotation (lastMoveDirection);
		} else
			return Quaternion.identity;

	}
	// The animation for the player's movement goes here
	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
			animator.SetFloat ("HorizontalVelocity", 0);
			animator.SetFloat ("VerticalVelocity", Mathf.Abs (horizontalInput) + Mathf.Abs (verticalInput));
	}

	public void PerformRoll(float horizontalInput, float verticalInput, Rigidbody rigidbody, float jumpSpeed, float rollSpeed) {
		rigidbody.AddRelativeForce (new Vector3(0, jumpSpeed / 2, rollSpeed), ForceMode.VelocityChange);
	}

	public void SetCameraSettings(MouseOrbit camera) {
		camera.yMinLimit = 35f;
		camera.yMaxLimit = 35f;
		camera.Distance = 5.5f;
		camera.gameObject.GetComponent<Camera> ().orthographic = true;
		camera.gameObject.GetComponent<Camera> ().orthographicSize = 3.8f;
	}

	public bool IsCrosshairEnabled() {
		return false;
	}
}