using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {


    private float attackCooldown = 0f;
    public float attackDelay = .6f;

    public event System.Action OnAttack;

    CharacterStats characterStats;

    private void Start() {
        characterStats = GetComponent<CharacterStats>();
    }

    private void Update() {
        attackCooldown -= Time.deltaTime;
    }

    public void AutoAttackMe() {

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
