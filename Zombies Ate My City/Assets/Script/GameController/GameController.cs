﻿using UnityEngine;
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
        levelClearImage.color = new Color(0, 0, 0, 0);
		exitDoor.Close ();
        gameOverAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		resourcesText.text = "Resources: " + resourcesCollected + "/" + resourcesAvailable;
		scoreText.text = "Score: " + score;

		if (resourcesCollected >= resourcesAvailable && !exitDoor.open) {
			exitDoor.Open ();
		}

        if (playerAlive.alive == false && !levelClear)
        {
            StartCoroutine(ShowClearScreen());
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
        yield return new WaitForSeconds (2f);
        levelClearImage.color = Color.Lerp(levelClearImage.color, new Color(0, 0, 0, 225f), Time.deltaTime * 2f);
        yield return new WaitForSeconds(2f);
        DestroyEnemies ();
		DisableUI ();
		CursorController cc = GetComponent<CursorController> ();
		cc.cursorMode = CursorLockMode.None;
		cc.visible = true;

		player.GetComponent<PlayerController> ().enabled = false;

        yield return new WaitForSeconds(2f);
        gameOverAudio.Play();
        gameOverText.text = "GAME OVER";

        yield return new WaitForSeconds(2f);
        gameOverAudio.Play();
        if (!playerAlive.alive)
        {
            gameStatusText.text = "YOU DIED";
        }
        else if (playerAlive.alive)
        {
            gameStatusText.text = "LEVEL CLEAR";
        }


        yield return new WaitForSeconds(2f);
        gameOverAudio.Play();
        resourcesEndText.text = "RESOURCES COLLECTED: " + resourcesCollected;

        yield return new WaitForSeconds(2f);
        gameOverAudio.Play();
        scoreEndText.text = "SCORE: " + score;

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
