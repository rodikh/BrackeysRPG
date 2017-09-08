using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

    public string displayName;
    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    public delegate void OnStatChanged();
    public OnStatChanged onStatChanged;


    public int GetValue() {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void SetBaseValue(int value) {
        baseValue = value;
        if (onStatChanged != null) {
            onStatChanged.Invoke();
        }
    }

    public void AddModifier(int modifier) {
        if (modifier != 0) {
            modifiers.Add(modifier);
            if (onStatChanged != null) {
                onStatChanged.Invoke();
            }
        }
    }

    public void RemoveModifier(int modifier) {
        if (modifier != 0) {
            modifiers.Remove(modifier);
            if (onStatChanged != null) {
                onStatChanged.Invoke();
            }
        }
    }
}
