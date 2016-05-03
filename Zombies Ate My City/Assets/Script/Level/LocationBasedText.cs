using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationBasedText : MonoBehaviour {
	public Text text;
	public bool destroyOnExit = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.enabled = false;
			if (destroyOnExit) {
				Destroy (gameObject);
			}
        }
    }
}
