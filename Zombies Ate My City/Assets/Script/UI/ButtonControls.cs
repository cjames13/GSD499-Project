using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonControls : MonoBehaviour {
	public string sceneName = "Final Prototype";
    public RawImage titleBackground, blankBackground, creditImage1, creditImage2;
    public GameObject backButton, exitButton, creditsButton, howButton, startButton;
    public GameObject creditsText;
    public AudioClip selectionClip;
    public AudioClip backClip;
    public AudioClip laughClip;

    AudioSource buttonSound;

    float speed = 10.0f;
    bool crawling = false;
    Vector2 creditsStartPos;

    void Start() {
        backButton.SetActive(false);
        creditImage1.enabled = false;
        creditImage2.enabled = false;
        creditsText.SetActive(false);
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

        titleBackground.enabled = false;
        backButton.SetActive(true);
        exitButton.SetActive(false);
        creditsButton.SetActive(false);
        howButton.SetActive(false);
        startButton.SetActive(false);

        creditImage1.enabled = false;
        creditImage2.enabled = false;
        creditsText.SetActive(false);
    }

    public void Credits() {
        buttonSound.PlayOneShot(selectionClip);

        titleBackground.enabled = false;
        backButton.SetActive(true);
        exitButton.SetActive(false);
        creditsButton.SetActive(false);
        howButton.SetActive(false);
        startButton.SetActive(false);

        crawling = true;

        creditImage1.enabled = true;
        creditImage2.enabled = true;
        creditsText.SetActive(true);
    }

	public void ExitGame() {
        buttonSound.PlayOneShot(selectionClip);

        Application.Quit ();
	}

    public void Back() {
        buttonSound.PlayOneShot(backClip, 2.0f);

        titleBackground.enabled = true;
        backButton.SetActive(false);
        exitButton.SetActive(true);
        creditsButton.SetActive(true);
        howButton.SetActive(true);
        startButton.SetActive(true);

        creditImage1.enabled = false;
        creditImage2.enabled = false;
        creditsText.SetActive(false);
        if (crawling)
        {
            crawling = false;
            creditsText.transform.position = new Vector3(creditImage1.transform.position.x, creditImage1.transform.position.y + 50, 0);
        }
    }

    IEnumerator PlayLaugh() {
        buttonSound.PlayOneShot(laughClip);
        titleBackground.enabled = false;
        blankBackground.enabled = false;
        backButton.SetActive(false);
        exitButton.SetActive(false);
        creditsButton.SetActive(false);
        howButton.SetActive(false);
        startButton.SetActive(false);

        creditImage1.enabled = false;
        creditImage2.enabled = false;
        creditsText.SetActive(false);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName);
    }
}
