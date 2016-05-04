using UnityEngine;
using System.Collections;

public class ThirdPersonControl : ControlStrategy {

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling) {
		Quaternion rotation;

		if (isRolling && moveDirection != Vector3.zero) {
			rotation = Quaternion.LookRotation (moveDirection);
		} else {
			rotation = Quaternion.Euler (0, camera.transform.eulerAngles.y, 0);
		}

		return rotation;
	}
}
