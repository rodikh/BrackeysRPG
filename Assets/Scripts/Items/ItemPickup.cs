using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;

    public override bool Interact(BaseCharacterController interactor) {
        base.Interact(interactor);

        Pickup();
        return true;
    }

    void Pickup() {
        Debug.Log("Picking up " + item.name);

        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) {
            Destroy(gameObject);
        }
    }
}
