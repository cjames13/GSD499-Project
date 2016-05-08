using UnityEngine;
using System.Collections;

public class ThirdPersonControl : ControlStrategy {
	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, 
		 float moveSpeed, float movementPenalty, bool isShooting) {
		float finalMoveSpeed = moveSpeed * ((Mathf.Abs (horizontalInput) > 0 || verticalInput < 0) ? movementPenalty : 1);
		return Vector3.ClampMagnitude (moveDirection * Time.deltaTime * finalMoveSpeed, finalMoveSpeed);
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling) {
		Quaternion rotation;

		if (isRolling && moveDirection != Vector3.zero) {
			rotation = Quaternion.LookRotation (moveDirection);
		} else {
			rotation = Quaternion.Euler (0, camera.transform.eulerAngles.y, 0);
		}

		return rotation;
	}

	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
		animator.SetFloat ("HorizontalVelocity", horizontalInput);
		animator.SetFloat ("VerticalVelocity", verticalInput);
	}

	public void PerformRoll(float horizontalInput, float verticalInput, Rigidbody rigidbody, float jumpSpeed, float rollSpeed) {
		rigidbody.AddRelativeForce (new Vector3 (horizontalInput * rollSpeed, jumpSpeed / 2f, verticalInput * rollSpeed), ForceMode.VelocityChange);
	}

	public void SetCameraSettings(MouseOrbit camera) {
		camera.yMinLimit = 20f;
		camera.yMaxLimit = 20f;
		camera.Distance = 4f;
	}

	public bool IsCrosshairEnabled() {
		return true;
	}
}
