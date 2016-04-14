using UnityEngine;
using System.Collections;

public class DrawRadarBlipUI : MonoBehaviour {
	public float searchDistance = 200f;
	public Color blipColor; 
	public int blipSize = 2;
	public string findTag = "Enemy";
	public Transform centerAround;
	public float mapScale = 0.3f;

	private Vector2 mapCenter;

	void Start() {
		mapCenter = GetComponent<RectTransform> ().sizeDelta;
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

			GUI.Box(new Rect(mapCenter.x + bX, mapCenter.y + bY, blipSize, blipSize), "a");
		}
	}
}
