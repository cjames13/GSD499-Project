﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitController : MonoBehaviour {
	public bool open = false;
    public Text doorText;

	private Animator anim;
	private GameController gameController;

	void Awake() {
		anim = GetComponent<Animator> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	public void Open() {
		anim.SetBool ("Open", true);
		open = true;
	}

	public void Close() {
		anim.SetBool ("Open", false);
        open = false;
    }

	void OnTriggerEnter(Collider other) {
		if (open && other.tag == "Player") {
			gameController.LevelOver(false);
		}
        else if (!open && other.tag == "Player") {
            doorText.text = "This could be our only way out...";
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (!open && other.tag == "Player") {
            doorText.text = "";
        }
    }
}
