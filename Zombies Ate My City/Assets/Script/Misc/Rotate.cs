﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
	}
}
