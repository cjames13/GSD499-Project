﻿using UnityEngine;
using System.Collections;

public class OverheadControl : ControlStrategy {
	Vector3 lastMoveDirection = Vector3.zero;
	// This sets the player's movement. Whatever value is returned from this function will be added to the player's current position
	public Vector3 SetPlayerMovement(float horizontalInput, float verticalInput, Vector3 moveDirection, 
		Vector3 normalMoveDirection, float moveSpeed, float movementPenalty, bool isShooting) {
		if (!isShooting)
			return Vector3.ClampMagnitude (moveDirection, moveSpeed) * Time.deltaTime * moveSpeed;
		else
			return Vector3.ClampMagnitude (normalMoveDirection, moveSpeed) * Time.deltaTime * moveSpeed;
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling, bool isShooting) {
		if (!isShooting && moveDirection != Vector3.zero) {
			lastMoveDirection = moveDirection;
			return Quaternion.LookRotation (moveDirection);
		} else if (lastMoveDirection != Vector3.zero) {
			return Quaternion.LookRotation (lastMoveDirection);
		} else
			return Quaternion.identity;

	}
	// The animation for the player's movement goes here
	public void SetPlayerMovementAnimation(Animator animator, float horizontalInput, float verticalInput, bool isShooting) {
		if (!isShooting) {
			animator.SetFloat ("HorizontalVelocity", 0);
			animator.SetFloat ("VerticalVelocity", Mathf.Abs (horizontalInput) + Mathf.Abs (verticalInput));
		}
		else {
			animator.SetFloat ("HorizontalVelocity", horizontalInput);
			animator.SetFloat ("VerticalVelocity", verticalInput);

		}
	}

	public void PerformRoll(float horizontalInput, float verticalInput, Rigidbody rigidbody, float jumpSpeed, float rollSpeed) {
		rigidbody.AddRelativeForce (new Vector3(0, jumpSpeed / 2, rollSpeed), ForceMode.VelocityChange);
	}

	public void SetCameraSettings(MouseOrbit camera) {
		camera.yMinLimit = 35f;
		camera.yMaxLimit = 35f;
		camera.Distance = 5.5f;
	}

	public bool IsCrosshairEnabled() {
		return false;
	}
}