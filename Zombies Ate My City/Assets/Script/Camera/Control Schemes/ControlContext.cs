using UnityEngine;

public class ControlContext {
	private ControlStrategy controls;

	public ControlContext(ControlStrategy strategy) {
		controls = strategy;
	}

	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling) {
		return controls.SetPlayerRotation (camera, moveDirection, isRolling);
	}

}