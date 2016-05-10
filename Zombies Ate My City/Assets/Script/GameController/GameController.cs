using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject player;
    public Text gameOverText, gameStatusText, resourcesText, resourcesEndText, scoreText, scoreEndText, killCountText, continueText;
	public ExitController exitDoor;
    public GameObject levelClearObject;
    public Image levelClearImage;
    public GameObject canvas;
    public bool allResourcesCollected;

	private int resourcesAvailable, resourcesCollected, score, enemiesKilled;
	private bool levelClear = false;
    private string startScreen = "Start Screen";
    private string restart = "Final Prototype";
    private AudioSource gameOverAudio;
    private bool endGame;

	// Use this for initialization
	void Start () {
		resourcesAvailable = GameObject.FindGameObjectsWithTag ("Resource").Length;
        levelClearObject.SetActive (false);
		exitDoor.Close ();
        gameOverAudio = GetComponent<AudioSource>();
		PlayerSettingsSingleton.Instance.controlContext.SetCameraSettings (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MouseOrbit>());
		foreach (GameObject ch in GameObject.FindGameObjectsWithTag("Crosshair")) {
			ch.SetActive (PlayerSettingsSingleton.Instance.controlContext.IsCrosshairEnabled ());
		}
	}

    // Update is called once per frame
    void Update() {
        resourcesText.text = "Resources: " + resourcesCollected + "/" + resourcesAvailable;
        scoreText.text = "Score: " + score;

        if (resourcesCollected >= resourcesAvailable && !exitDoor.open) {
            exitDoor.Open();
            allResourcesCollected = true;
        }

        if (endGame)
        {
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene(startScreen);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(restart);
            }
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

	public void LevelOver(bool dead) {
		if (!levelClear) {
			levelClear = true;
			exitDoor.Close ();
			StartCoroutine (ShowClearScreen (dead));
		}

	}

	IEnumerator ShowClearScreen(bool dead) {
        yield return new WaitForSeconds(4f);
        DestroyEnemies ();
		DisableUI ();
		CursorController cc = GetComponent<CursorController> ();
		cc.cursorMode = CursorLockMode.None;
		cc.visible = true;

		player.GetComponent<PlayerController> ().enabled = false;

		levelClearObject.SetActive (true);
        gameOverAudio.Play();
        gameOverText.text = "GAME OVER";

        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
		gameStatusText.text = (dead) ? "YOU DIED" : "LEVEL CLEAR";

		yield return new WaitForSeconds(1f);
		gameOverAudio.Play();
        resourcesEndText.text = "RESOURCES COLLECTED: " + resourcesCollected + "/" + resourcesAvailable;

        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
        scoreEndText.text = "SCORE: " + score;

        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
        killCountText.text = "KILLS: " + enemiesKilled;

        yield return new WaitForSeconds(1f);
        continueText.text = "Press 'R' to Restart the level." + "\nPress 'M' to return to the Main Menu.";

        endGame = true;
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
