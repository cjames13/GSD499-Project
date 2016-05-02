using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject player;
	public Text resourcesText, scoreText;
	public ExitController exitDoor;
	public GameObject levelClearObject;
	public GameObject canvas;

	private int resourcesAvailable, resourcesCollected, score, enemiesKilled;
	private bool levelClear = false;
    private string sceneName = "Start Screen";

	// Use this for initialization
	void Start () {
		resourcesAvailable = GameObject.FindGameObjectsWithTag ("Resource").Length;
		levelClearObject.SetActive (false);
		exitDoor.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		resourcesText.text = "Resources: " + resourcesCollected + "/" + resourcesAvailable;
		scoreText.text = "Score: " + score;

		if (resourcesCollected >= resourcesAvailable && !exitDoor.open) {
			exitDoor.Open ();
		}

        if (levelClear && Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene(sceneName);
        }
	}

	public void ResourceCollected() {
		resourcesCollected++;
	}

	public void IncreaseScore(int s) {
		score += s;
	}

	public void IncreaseKills(int n) {
		enemiesKilled += n;
	}

	public void WinLevel() {
		if (!levelClear) {
			levelClear = true;
			exitDoor.Close ();
			StartCoroutine (ShowClearScreen ());
		}

	}

	IEnumerator ShowClearScreen() {
		yield return new WaitForSeconds (1);
		DestroyEnemies ();
		DisableUI ();
		CursorController cc = GetComponent<CursorController> ();
		cc.cursorMode = CursorLockMode.None;
		cc.visible = true;

		player.GetComponent<PlayerController> ().enabled = false;

		levelClearObject.SetActive (true);
		levelClearObject.GetComponentInChildren<Text>().text = "You survived.\n\nScore: " + score +
								"\nResources Collected: " + resourcesCollected +
								"\nKills: " + enemiesKilled +
								"\n\nPress 'M' to return to the Main Menu.";
	}

	void DestroyEnemies() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}
	}

	void DisableUI() {
		foreach (Transform child in canvas.transform) {
			child.gameObject.SetActive (false);
		}
	}
}
