using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CityWall : MonoBehaviour {

    public Text cityWallText;

    void Awake()
    {
        cityWallText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cityWallText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cityWallText.enabled = false;
        }
    }
}
