using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public Transform target;
	NavMeshAgent agent;
	StateController enemyStates;
	// Use this for initialization
	void Start () {
		enemyStates = GetComponent<StateController> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (target.position);
		enemyStates.Walk ();
		if (agent.remainingDistance <= agent.stoppingDistance)
			enemyStates.Attack(agent);
		if (name == "Necromancer") {
			enemyStates.Attack (agent);
			enemyStates.MagicAttack (agent);
		}
		
	}
}
