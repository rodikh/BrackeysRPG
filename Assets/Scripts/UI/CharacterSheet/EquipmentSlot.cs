using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour {
    public Image icon;
    Equipment item;
    public EquipmentSlotEnum slotIndex;

    public Button removeButton;

    public void AddItem(Equipment newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() {
        EquipmentManager.instance.Unequip((int)item.equipSlot, true);
    }
}
