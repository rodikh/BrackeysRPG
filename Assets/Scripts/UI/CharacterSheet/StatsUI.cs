using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {

    public PlayerStats playerStats;

    Text[] statLabels;
    PropertyInfo[] properties;

    private void Start() {
        statLabels = GetComponentsInChildren<Text>();
        UpdateUI();
        playerStats.onStatChanged += UpdateUI;
    }

    private void UpdateUI() {
        Stat[] stats = playerStats.displayedStats;
        for (int i = 0; i < stats.Length; i++) {
            statLabels[i].text = stats[i].displayName +": "+stats[i].GetValue();
        }
    }
}
