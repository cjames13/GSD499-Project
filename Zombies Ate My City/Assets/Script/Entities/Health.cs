using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
	public int maxHealth;
	public int currentHealth;
	public float invulnTimeAfterHit = 0f;
	public bool alive = true;

	private StateController animController;
	private float lastHitTime;

    public AudioClip entityHurt;
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
	}

	public void Damage(int d) {
        if (Time.time - lastHitTime >= invulnTimeAfterHit) {
            currentHealth -= d;
            animController.TakeDamage();
            lastHitTime = Time.time;
            if (!entityAudio.isPlaying)
            {
                entityAudio.PlayOneShot(entityHurt);
            }
		}
	}
}
