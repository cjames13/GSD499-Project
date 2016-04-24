using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {
	public CursorLockMode cursorMode = CursorLockMode.Locked;
	public bool visible = false;

	private bool cursorLocked = true;

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			cursorLocked = true;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			cursorLocked = false;
		}

		if (cursorLocked) {
			Cursor.lockState = cursorMode;
			Cursor.visible = visible;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
