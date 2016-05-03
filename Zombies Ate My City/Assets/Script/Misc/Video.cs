using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class Video : MonoBehaviour
{
    public Text videoSkipText;
    public MovieTexture movie;
    private new AudioSource audio;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();

        movie.Play();
        audio.Play();       
    }

    void Update()
    {
        StartCoroutine(VideoSkip());
        if (!movie.isPlaying || Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Start Screen");
        }
    }

    IEnumerator VideoSkip()
    {
        yield return new WaitForSeconds(5);
        videoSkipText.text = "Press 'S' to Skip";
    }



}
