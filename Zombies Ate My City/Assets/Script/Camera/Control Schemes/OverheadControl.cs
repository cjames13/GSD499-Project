using UnityEngine;
using System.Collections;

public class OverheadControl : ControlStrategy {
	public Quaternion SetPlayerRotation(Camera camera, Vector3 moveDirection, bool isRolling) {
		return Quaternion.identity;
	}
}