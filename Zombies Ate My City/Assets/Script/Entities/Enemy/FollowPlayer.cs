using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public Transform target;
	NavMeshAgent agent;
	StateController enemyStates;

	float magicAttackDistance = 8;
	// Use this for initialization
	void Start () {
		enemyStates = GetComponent<StateController> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		float attackDistance = Vector3.Distance (agent.nextPosition, target.position);
		agent.SetDestination (target.position);
		Vector3 lookPos = target.position - transform.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation (lookPos);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 3);
		enemyStates.Walk ();
		if (Vector3.Distance (agent.nextPosition, target.position) <= agent.stoppingDistance)
			enemyStates.Attack (true);
		else
			enemyStates.Attack (false);
		if (name == "Necromancer") {
			if (attackDistance <= agent.stoppingDistance)
				enemyStates.Attack (true);
			else
				enemyStates.Attack (false);
			if (attackDistance <= agent.stoppingDistance + magicAttackDistance && 
				attackDistance >= agent.stoppingDistance)
				enemyStates.RangedAttack (true);
			else
				enemyStates.RangedAttack (false);
		}
		
	}
}
