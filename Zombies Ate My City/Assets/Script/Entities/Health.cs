using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
	public float maxHealth;
	public float currentHealth;
	public float invulnTimeAfterHit = 0f;
	public bool alive = true;

	public float healTime = 1f;
	private float healUntil = 0f;
	private float currentHealTime = 0f;
	private bool isHealing = false;

    private StateController animController;
	private float lastHitTime = 0f;

    public AudioClip entityHurt;
	public AudioClip entityHeal;
    public AudioClip entityDeath;

    private AudioSource entityAudio;

	// Use this for initialization
	void Start () {
		animController = GetComponent<StateController> ();
		currentHealth = maxHealth;
		lastHitTime = Time.time;
        entityAudio = GetComponent<AudioSource>();
    }

	void Update() {
		if (currentHealth <= 0 && alive) {
			alive = false;
			animController.Die ();
            entityAudio.PlayOneShot(entityDeath);
		}

		if (isHealing) {
			currentHealTime = (currentHealTime + Time.deltaTime > healTime) ? healTime : currentHealTime + Time.deltaTime;
			float p = currentHealTime / healTime;
			currentHealth = Mathf.Lerp (currentHealth, healUntil, p);
			if (currentHealth >= healUntil) {
				isHealing = false;
			}
		}
	}

	public void Heal(float h) {
		isHealing = true;
		healUntil = (currentHealth + h > maxHealth) ? maxHealth : currentHealth + h; 
		if (entityHeal != null) {
			entityAudio.PlayOneShot (entityHeal, .5f);
		}
	}

	public void Damage(int d) {
        if (Time.time - lastHitTime >= invulnTimeAfterHit) {
            currentHealth -= d;
            animController.TakeDamage();
            lastHitTime = Time.time;
            if (!entityAudio.isPlaying && alive)
            {
                entityAudio.PlayOneShot(entityHurt);
            }
		}
	}
}
