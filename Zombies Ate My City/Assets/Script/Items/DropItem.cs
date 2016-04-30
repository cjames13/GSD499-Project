using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour {
	public GameObject[] items;

	[Range(0, 100)]
	public float percentChance = 0f;

	// Update is called once per frame
	void Update () {
	
	}
}
