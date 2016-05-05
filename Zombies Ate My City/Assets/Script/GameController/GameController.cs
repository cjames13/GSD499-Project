using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject player;
	public Text gameOverText, gameStatusText, resourcesText, resourcesEndText, scoreText, scoreEndText;
	public ExitController exitDoor;
    //public GameObject levelClearObject;
    public Image levelClearImage;
    public GameObject canvas;
    private Color levelClearColor = new Color(0f, 0f, 0f, 1f);

	private int resourcesAvailable, resourcesCollected, score, enemiesKilled;
	private bool levelClear = false;
    private string sceneName = "Start Screen";
    private AudioSource gameOverAudio;
    private Health playerAlive;
    private bool endGame;

	// Use this for initialization
	void Start () {
		resourcesAvailable = GameObject.FindGameObjectsWithTag ("Resource").Length;
        playerAlive = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        //levelClearObject.SetActive (false);
        levelClearImage.color = new Color(0f, 0f, 0f, 0f);
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
        }

        if (!playerAlive.alive)
        {
            LevelOver();
        }
    
        if (endGame && Input.GetKey(KeyCode.M))
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

	public void LevelOver() {
		if (!levelClear) {
			levelClear = true;
			exitDoor.Close ();
			StartCoroutine (ShowClearScreen ());
		}
        else if (!levelClear && !playerAlive.alive)
        {
            StartCoroutine(ShowClearScreen());
        }

	}

	IEnumerator ShowClearScreen() {
        yield return new WaitForSeconds (1f);
        levelClearImage.color = Color.Lerp(levelClearImage.color, levelClearColor, Time.deltaTime * 5f);
        yield return new WaitForSeconds(2f);
        DestroyEnemies ();
		DisableUI ();
		CursorController cc = GetComponent<CursorController> ();
		cc.cursorMode = CursorLockMode.None;
		cc.visible = true;

		player.GetComponent<PlayerController> ().enabled = false;

        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
        gameOverText.text = "GAME OVER";
        Debug.Log("Game Over");

        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
        if (!levelClear)
        {
            gameStatusText.text = "YOU DIED";
        }
        else if (levelClear)
        {
            gameStatusText.text = "LEVEL CLEAR";
        }
        Debug.Log("You Died");


        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
        resourcesEndText.text = "RESOURCES COLLECTED: " + resourcesCollected;
        Debug.Log("Resources Collected");

        yield return new WaitForSeconds(1f);
        gameOverAudio.Play();
        scoreEndText.text = "SCORE: " + score;
        Debug.Log("Final Score");

        endGame = true;
        /*levelClearObject.SetActive (true);
		levelClearObject.GetComponentInChildren<Text>().text = "You survived.\n\nScore: " + score +
								"\nResources Collected: " + resourcesCollected +
								"\nKills: " + enemiesKilled +
								"\n\nPress 'M' to return to the Main Menu.";/*/
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
