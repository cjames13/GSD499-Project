using UnityEngine;
using System.Collections;

public class ThirdPersonControl : ControlStrategy {
	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, float moveSpeed, float movementPenalty) {
		float finalMoveSpeed = moveSpeed * ((Mathf.Abs (horizontalInput) > 0 || verticalInput < 0) ? movementPenalty : 1);
		return Vector3.ClampMagnitude (moveDirection * Time.deltaTime * finalMoveSpeed, finalMoveSpeed);
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling, bool isShooting) {
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
}
