using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempScript : MonoBehaviour {

    public Text tempText;

    void Awake()
    {
        tempText.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tempText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tempText.enabled = false;
        }
    }
}
