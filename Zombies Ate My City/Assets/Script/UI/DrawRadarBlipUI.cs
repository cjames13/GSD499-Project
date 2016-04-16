﻿using UnityEngine;
using System.Collections;

public class DrawRadarBlipUI : MonoBehaviour {
	public float searchDistance = 200f;
	public Color blipColor; 
	public int blipSize = 2;
	public string findTag = "Enemy";
	public Transform centerAround;
	public float mapScale = 0.3f;
	public Vector2 mapCenter = new Vector2 (142, 97);

	private Texture2D blipTexture;

	void Start() {
		CreateBlipTexture ();
	}

	void CreateBlipTexture() {
		blipTexture = new Texture2D (blipSize, blipSize);
		Color[] c = blipTexture.GetPixels ();

		for (int i = 0; i < c.Length; i++) {
			c[i] = blipColor;
		}

		blipTexture.SetPixels (c);
		blipTexture.Apply ();
	}

	void OnGUI() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag (findTag);
		foreach (GameObject obj in objs) {
			DrawBlip (obj);
		}
	}

	void DrawBlip(GameObject obj) {
		Vector3 center = centerAround.position;
		Vector3 dest = obj.transform.position;

		float distance = Vector3.Distance (center, dest);

		if (distance <= searchDistance) {
			float dX = center.x - dest.x;
			float dZ = center.z - dest.z;
			float turnAngle = Mathf.Atan2 (dX, dZ) * Mathf.Rad2Deg - 270 - centerAround.eulerAngles.y;

			float bX = distance * Mathf.Cos (turnAngle * Mathf.Deg2Rad);
			float bY = distance * Mathf.Sin (turnAngle * Mathf.Deg2Rad);


			bX *= mapScale;
			bY *= mapScale;

			GUI.DrawTexture(new Rect(mapCenter.x + bX, mapCenter.y + bY, blipSize, blipSize), blipTexture);
	
		}
	}
}