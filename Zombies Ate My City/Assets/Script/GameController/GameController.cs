using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject player;
	public Text resourcesText, scoreText;
	public ExitController exitDoor;
	public GameObject levelClearScreen;
	public Text levelClearText;
	public GameObject radar;

	private int resourcesAvailable, resourcesCollected, score, enemiesKilled;
	private bool levelClear = false;
    private string sceneName = "Start Screen";

	// Use this for initialization
	void Start () {
		resourcesAvailable = GameObject.FindGameObjectsWithTag ("Resource").Length;
		levelClearScreen.SetActive (false);
		levelClearText.enabled = false;
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
		levelClearScreen.SetActive (true);
		levelClearText.enabled = true;
		radar.SetActive (false);

		levelClearText.text = "You survived.\n\nScore: " + score +
								"\nResources Collected: " + resourcesCollected +
								"\nKills: " + enemiesKilled +
								"\n\nPress 'M' to return to the Main Menu.";
	}
}
