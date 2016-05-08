using UnityEngine;

public class ControlContext {
	private ControlStrategy controls;

	public ControlContext(ControlStrategy strategy) {
		controls = strategy;
	}

	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection,
		Vector3 normalMoveDirection, float moveSpeed, float movementPenalty, bool isShooting) {
		return controls.SetPlayerMovement (horizontalInput, verticalInput, moveDirection, normalMoveDirection,
			moveSpeed, movementPenalty, isShooting);
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling, bool isShooting) {
		return controls.SetPlayerRotation (camera, moveDirection, isRolling, isShooting);
	}

	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
		controls.SetPlayerMovementAnimation (animator, horizontalInput, verticalInput, isShooting);
	}

	public void PerformRoll(float horizontalInput, float verticalInput, Rigidbody rigidbody, float jumpSpeed, float rollSpeed) {
		controls.PerformRoll (horizontalInput, verticalInput, rigidbody, jumpSpeed, rollSpeed);
	}

	public void SetCameraSettings(MouseOrbit camera) {
		controls.SetCameraSettings (camera);
	}

	public bool IsCrosshairEnabled() {
		return controls.IsCrosshairEnabled ();
	}
}