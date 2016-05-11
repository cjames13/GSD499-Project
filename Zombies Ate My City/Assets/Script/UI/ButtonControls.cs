using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonControls : MonoBehaviour {
	public string sceneName = "Final Prototype";
    public RawImage titleBackground, blankBackground, creditImage1, creditImage2;
    public GameObject backButton, optionsButton, creditsButton, howButton, startButton, optionsScreen, exitButton;
    public GameObject creditsText, howToText;
    public AudioClip selectionClip;
    public AudioClip backClip;
    public AudioClip laughClip;

    AudioSource buttonSound;

    float speed = 25.0f;
    bool crawling = false;
    Vector2 creditsStartPos;

    void Start() {
        backButton.SetActive(false);
        creditImage1.enabled = false;
        creditImage2.enabled = false;
		creditsText.SetActive(false);
		howToText.SetActive(false);
        buttonSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (crawling)
        {
            creditsText.transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (creditsText.transform.position.y >= 700)
            {
                crawling = false;
                creditsText.transform.position = new Vector3(creditImage1.transform.position.x, creditImage1.transform.position.y + 50, 0);
            }
        }
           
    }

	public void LoadGame() {
        StartCoroutine(PlayLaugh());
	}

    public void HowToPlay() {
        buttonSound.PlayOneShot(selectionClip);
		SetAllElementsInactive ();
        titleBackground.enabled = false;
        backButton.SetActive(true);


		howToText.SetActive (true);
    }

    public void Credits() {
        buttonSound.PlayOneShot(selectionClip);

        titleBackground.enabled = false;

		SetAllElementsInactive ();

		backButton.SetActive (true);
        crawling = true;

        creditImage1.enabled = true;
        creditImage2.enabled = true;
        creditsText.SetActive(true);
    }

	public void Options() {
        buttonSound.PlayOneShot(selectionClip);
		SetAllElementsInactive ();
		titleBackground.enabled = false;
		optionsScreen.SetActive (true);
		backButton.SetActive (true);


	}

	public void ChangeCameraControls(int c) {
		switch (c) {
		case 0:
			PlayerSettingsSingleton.Instance.controlContext = new ControlContext (new ThirdPersonControl ());
			break;
		case 1:
			PlayerSettingsSingleton.Instance.controlContext = new ControlContext (new OverheadControl ());
			break;
		}
	}

    public void Back() {
        buttonSound.PlayOneShot(backClip, 2.0f);

        titleBackground.enabled = true;
        backButton.SetActive(false);
        optionsButton.SetActive(true);
        creditsButton.SetActive(true);
        howButton.SetActive(true);
        startButton.SetActive(true);
        exitButton.SetActive(true);
		optionsScreen.SetActive (false);

        creditImage1.enabled = false;
        creditImage2.enabled = false;
        creditsText.SetActive(false);

		howToText.SetActive (false);

        if (crawling)
        {
            crawling = false;
            creditsText.transform.position = new Vector3(creditImage1.transform.position.x, creditImage1.transform.position.y + 50, 0);
        }
    }

    public void Exit() {
        Application.Quit();
    }

    IEnumerator PlayLaugh() {
        buttonSound.PlayOneShot(laughClip);
        titleBackground.enabled = false;
        blankBackground.enabled = false;
		SetAllElementsInactive ();

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(sceneName);
    }

	void SetAllElementsInactive() {
		backButton.SetActive(false);
		optionsButton.SetActive(false);
		creditsButton.SetActive(false);
		howButton.SetActive(false);
		startButton.SetActive(false);
		optionsScreen.SetActive (false);
        exitButton.SetActive(false);

		creditImage1.enabled = false;
		creditImage2.enabled = false;
		creditsText.SetActive(false);
	}
}
