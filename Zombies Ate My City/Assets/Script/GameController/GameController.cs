using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject player;
	public Text resourcesText, scoreText;

	private int resourcesAvailable, resourcesCollected, score;

	// Use this for initialization
	void Start () {
		resourcesAvailable = GameObject.FindGameObjectsWithTag ("Resource").Length;
	}
	
	// Update is called once per frame
	void Update () {
		resourcesText.text = "Resources: " + resourcesCollected + "/" + resourcesAvailable;
		scoreText.text = "Score: " + score;
	}

	public void ResourceCollected() {
		resourcesCollected++;
	}
}
