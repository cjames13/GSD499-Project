using UnityEngine;

public interface ControlStrategy {
	Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, float moveSpeed, float movementPenalty);
	Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling, bool isShooting);
	void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting);
	void PerformRoll(float horizontalInput, float verticalInput, Rigidbody rigidbody, float jumpSpeed, float rollSpeed);
	void SetCameraSettings(MouseOrbit camera);
	bool IsCrosshairEnabled();
}