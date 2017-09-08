using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheetUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject characterSheetUI;

    EquipmentSlot[] slots;

    EquipmentManager equipmentManager;

    // Use this for initialization
    void Start() {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("CharacterSheet")) {
            characterSheetUI.SetActive(!characterSheetUI.activeSelf);
        }
    }

    void UpdateUI(Equipment newItem, Equipment oldItem) {
        Debug.Log("Updating CS-UI");
        for (int i = 0; i < slots.Length; i++) {
            if (equipmentManager.currentEquipment[i] != null && !equipmentManager.currentEquipment[i].isDefaultItem) {
                slots[i].AddItem(equipmentManager.currentEquipment[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }
}
