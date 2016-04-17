using UnityEngine;
using System.Collections;

public interface StateController {
	void TakeDamage();
	void Attack(bool attacking);
	void RangedAttack(bool attacking);
	void Walk();
	void Die();
}