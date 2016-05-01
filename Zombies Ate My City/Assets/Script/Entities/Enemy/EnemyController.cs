﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject target;
	public bool rangedAttacker = false;
	public GameObject projectileObject;
	public float magicAttackDistance = 8f;
	public float magicAttackDelay = 2f;
	public float rotationSpeed = 3f;
	public float attackSpeed = 1f;
	public int scoreValue;

	private Animator anim;
	private NavMeshAgent agent;
	private StateController enemyStates;
	private Health health;
	private float magicAttackTime;

	void Start () {
		enemyStates = GetComponent<StateController> ();
		health = GetComponent<Health> ();
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

		if (rangedAttacker) {
			magicAttackTime = Time.time;
		}

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

			if (rangedAttacker && attackDistance <= agent.stoppingDistance + magicAttackDistance &&
				attackDistance >= agent.stoppingDistance && Time.time - magicAttackTime > magicAttackDelay) {
				magicAttackTime = Time.time;
				enemyStates.RangedAttack (true, false);
				Instantiate (projectileObject,
					new Vector3 (transform.position.x, transform.position.y + 0.5f,
						transform.position.z - 1), transform.rotation);
			}
		} else {
			agent.velocity = Vector3.zero;
			gameObject.GetComponent<NavMeshAgent> ().enabled = false;
		}
	}
}
