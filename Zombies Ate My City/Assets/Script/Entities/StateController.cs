using UnityEngine;
using System.Collections;

public interface StateController {
	void TakeDamage();
	void MeleeAttack(bool attacking);
	void RangedAttack(bool attacking, bool isRifle);
	void ThrownAttack(bool attacking);
	void Walk();
	void Die();
}