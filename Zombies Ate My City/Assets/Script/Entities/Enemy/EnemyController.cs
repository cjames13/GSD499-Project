using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject target;
	public bool rangedAttacker = false;
	public float magicAttackDistance = 8f;
	public float rotationSpeed = 3f;
	public float attackSpeed = 1f;
	public int scoreValue;

	private Animator anim;
	private NavMeshAgent agent;
	private StateController enemyStates;
	private Health health;

	void Start () {
		enemyStates = GetComponent<StateController> ();
		health = GetComponent<Health> ();
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	

	void Update () {
		if (!anim.GetBool ("dying") && health.alive) {
			float attackDistance = Vector3.Distance (agent.nextPosition, target.transform.position);
			agent.SetDestination (target.transform.position);
			Vector3 lookPos = target.transform.position - transform.position;
			lookPos.y = 0;
			if (lookPos != Vector3.zero) {
				Quaternion rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			}
			enemyStates.Walk ();

			if (attackDistance < agent.stoppingDistance) {
				enemyStates.MeleeAttack (true);
			} else {
				enemyStates.MeleeAttack (false);
			}

			if (rangedAttacker) {
				if (attackDistance <= agent.stoppingDistance)
					enemyStates.MeleeAttack (true);
				else
					enemyStates.MeleeAttack (false);
				if (attackDistance <= agent.stoppingDistance + magicAttackDistance &&
				    attackDistance >= agent.stoppingDistance)
					enemyStates.RangedAttack (true, false);
				else
					enemyStates.RangedAttack (false, false);
			}
		
		} else {
			agent.velocity = Vector3.zero;
			gameObject.GetComponent<NavMeshAgent> ().enabled = false;
		}
	}
}
