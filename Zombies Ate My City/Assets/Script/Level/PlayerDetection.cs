using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour {

    Spawner spawnerScript;

    void Start()
    {
        spawnerScript = GetComponent<Spawner>();
        spawnerScript.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnerScript.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnerScript.enabled = false;
        }
    }
}
