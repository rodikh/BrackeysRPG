using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    public Transform interactionTransform;

    public virtual bool Interact (BaseCharacterController interactor) {
        return false;
        // This is meant to be overwritten;
        //Debug.Log("Interacting with " + transform.name);
    }

    private void Update() {
        //if (!hasInteracted) {
        //    float distance = Vector3.Distance(player.position, interactionTransform.position);
        //    if (distance <= radius) {
        //        Interact();
        //        hasInteracted = true;
        //    }
        //}
    }

    void OnDrawGizmosSelected () {
        if (interactionTransform == null) {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
