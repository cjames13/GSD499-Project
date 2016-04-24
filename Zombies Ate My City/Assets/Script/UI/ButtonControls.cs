using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonControls : MonoBehaviour {
	public string sceneName = "Final Prototype";

	public void LoadGame() {
		SceneManager.LoadScene (sceneName);
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
