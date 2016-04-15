using UnityEngine;
using System.Collections;
public interface StateController {
	void TakeDamage();
	void Die();
	void Attack(NavMeshAgent agent);
	void MagicAttack(NavMeshAgent agent);
	void Walk();
	void Run();
}