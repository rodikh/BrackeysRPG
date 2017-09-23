using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    public Transform interactionTransform;

    public virtual bool Interact (BaseCharacterController interactor) {
        return false;
    }

    void OnDrawGizmosSelected () {
        if (interactionTransform == null) {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
