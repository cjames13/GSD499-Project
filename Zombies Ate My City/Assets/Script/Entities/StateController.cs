using UnityEngine;
using System.Collections;

public interface StateController {
	void TakeDamage();
	void Die();
	void Attack(bool attacking);
	void RangedAttack(bool attacking);
	void Walk();
	void Run();
}