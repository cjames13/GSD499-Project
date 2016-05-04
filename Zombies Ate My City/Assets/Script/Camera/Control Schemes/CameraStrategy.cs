using UnityEngine;

public interface ControlStrategy {
	Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling);
}