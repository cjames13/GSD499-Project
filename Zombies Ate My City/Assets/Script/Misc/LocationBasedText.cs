using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationBasedText : MonoBehaviour {

    public Text text;

    void Awake()
    {
        text.enabled = false;
    }

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
        }
    }
}
