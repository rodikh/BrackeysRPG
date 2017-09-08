using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public Stat maxHealth;
    public Stat damage;
    public Stat armor;

    public Stat[] displayedStats;
    public delegate void OnStatChanged();
    public OnStatChanged onStatChanged;

    public int currentHealth { get; private set; }
    private void Awake() {
        currentHealth = maxHealth.GetValue();
        displayedStats = new Stat[] { maxHealth, damage, armor };
        for (int i = 0; i < displayedStats.Length; i++) {
            displayedStats[i].onStatChanged += RegisterStatChanged;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(10);
        }
    }

    public void TakeDamage (int damage) {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }

    public void RegisterStatChanged() {
        if (onStatChanged != null) {
            onStatChanged.Invoke();
        }
    }
}
