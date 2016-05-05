using UnityEngine;

public class ControlContext {
	private ControlStrategy controls;

	public ControlContext(ControlStrategy strategy) {
		controls = strategy;
	}

	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, float moveSpeed, float movementPenalty) {
		return controls.SetPlayerMovement (horizontalInput, verticalInput, moveDirection, moveSpeed, movementPenalty);
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling, bool isShooting) {
		return controls.SetPlayerRotation (camera, moveDirection, isRolling, isShooting);
	}

	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
		controls.SetPlayerMovementAnimation (animator, horizontalInput, verticalInput, isShooting);
	}

}