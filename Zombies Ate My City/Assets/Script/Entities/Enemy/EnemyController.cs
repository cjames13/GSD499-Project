using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject target;
	public bool rangedAttacker = false;
	public float magicAttackDistance = 8f;
	public float rotationSpeed = 3f;
	public float attackSpeed = 1f;
	public int scoreValue;

	private NavMeshAgent agent;
	private StateController enemyStates;

	void Start () {
		enemyStates = GetComponent<StateController> ();
		agent = GetComponent<NavMeshAgent> ();

		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	

	void Update () {
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
				enemyStates.RangedAttack (true);
			else
				enemyStates.RangedAttack (false);
		}
		
	}
}
