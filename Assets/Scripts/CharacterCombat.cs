using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {


    private float attackCooldown = 0f;
    public float attackDelay = .6f;

    public event System.Action OnAttack;
    public CharacterStats autoAttackTarget;

    NavMeshAgent agent;

    CharacterStats characterStats;

    private void Start() {
        characterStats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        attackCooldown -= Time.deltaTime;

        if (autoAttackTarget != null && Vector3.Distance(transform.position, autoAttackTarget.transform.position) <= agent.stoppingDistance) {
            Attack(autoAttackTarget);
        }

    }

    public void AutoAttack(CharacterStats target) {
        autoAttackTarget = target;
    }

    public void StopAutoAttack() {
        autoAttackTarget = null;
    }

    public void Attack (CharacterStats targetStats) {
        if (attackCooldown < 0f) {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null) {
                OnAttack();
            }

            attackCooldown = 1f / characterStats.attackSpeed.GetValue();
        }
        
    }

    IEnumerator DoDamage (CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(characterStats.damage.GetValue());
    }
}
