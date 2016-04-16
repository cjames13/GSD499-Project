using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarUI : MonoBehaviour {
	public Health health;
	public RectTransform healthBar;
	public int margin = 4;

	private RectTransform myRect;

	void Start() {
		myRect = GetComponent<RectTransform> ();
	}

	void Update () {
		float healthPercent = (float)health.currentHealth / (float)health.maxHealth;
		healthBar.sizeDelta = new Vector2((myRect.sizeDelta.x - margin) * healthPercent, healthBar.sizeDelta.y);
	}
}
