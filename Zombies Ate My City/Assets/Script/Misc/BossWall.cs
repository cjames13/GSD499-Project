using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossWall : MonoBehaviour {

    public Text bossWallText;

    void Awake()
    {
        bossWallText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossWallText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossWallText.enabled = false;
        }
    }
}
