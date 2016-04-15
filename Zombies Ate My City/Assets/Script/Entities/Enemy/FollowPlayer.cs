using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public Transform target;
	NavMeshAgent agent;
	StateController enemyStates;
	float attackDistance;
	float magicAttackDistance = 10;
	// Use this for initialization
	void Start () {
		enemyStates = GetComponent<StateController> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		attackDistance = Vector3.Distance (agent.nextPosition, target.position);
		agent.SetDestination (target.position);
		enemyStates.Walk ();
		if (Vector3.Distance(agent.nextPosition, target.position) <= agent.stoppingDistance)
			enemyStates.Attack(agent);
		if (name == "Necromancer") {
			if (attackDistance <= agent.stoppingDistance)
			enemyStates.Attack (agent);
			if (attackDistance <= agent.stoppingDistance + magicAttackDistance)
			enemyStates.MagicAttack (agent);
		}
		
	}
}
