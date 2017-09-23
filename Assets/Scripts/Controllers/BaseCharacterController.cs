using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseCharacterController : MonoBehaviour {
    
    protected NavMeshAgent agent;
    protected CharacterCombat combat;
    protected Interactable focus;
    protected Transform target;
    protected bool hasInteracted = false;

    // Use this for initialization
    protected void Start () {
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    private void Update() {
        if (focus != null && hasInteracted == false) {
            agent.SetDestination(target.position);
            if (Vector3.Distance(transform.position, target.position) < focus.radius) {
                FaceTarget();
                if (hasInteracted == false) {
                    hasInteracted = focus.Interact(this);
                }
            }
        } else if (hasInteracted == true) {
            RemoveFocus();
        }
    }

    public void MoveToPoint(Vector3 point) {
        agent.SetDestination(point);
    }


    public void SetFocus(Interactable newFocus) {
        Debug.Log("setting focus" + newFocus + focus);
        focus = newFocus;
        hasInteracted = false;
        FollowTarget(focus);
    }

    public void RemoveFocus() {
        focus = null;
        hasInteracted = false;
        StopFollowingTarget();
    }

    public void FollowTarget(Interactable newTarget) {
        if (newTarget == null) {
            return;
        }
        agent.stoppingDistance = newTarget.radius * .8f;
        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget() {
        target = null;
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
    }

    protected void FaceTarget() {
        if (target == null) {
            return;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
