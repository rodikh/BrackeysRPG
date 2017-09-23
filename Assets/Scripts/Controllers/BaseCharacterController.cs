using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseCharacterController : Interactable {
    
    protected NavMeshAgent agent;
    protected CharacterCombat combat;
    protected Interactable focus;
    protected Transform target;
    protected bool hasInteracted = false;
    public FactionsEnum faction;

    // Use this for initialization
    protected void Start () {
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    private void Update() {
        if (hasInteracted == true) {
            RemoveFocus();
            hasInteracted = false;
        }

        if (focus != null) {
            agent.SetDestination(target.position);
            if (Vector3.Distance(transform.position, target.position) < focus.radius) {
                FaceTarget();
                hasInteracted = focus.Interact(this);
            }
        } else if (focus == null && target != null) {
            Debug.Log("Have target but no focus");
            RemoveFocus();
        }
    }

    public void MoveToPoint(Vector3 point) {
        agent.SetDestination(point);
    }


    public void SetFocus(Interactable newFocus) {
        focus = newFocus;
        hasInteracted = false;
        FollowTarget(focus);

        BaseCharacterController targetCharController = focus.GetComponent<BaseCharacterController>();
        if (targetCharController != null && targetCharController.faction != faction) {
            Debug.Log(transform.name + ": AutoAttacking " + focus.transform.name);
            combat.AutoAttack(focus.GetComponent<CharacterStats>());
        }
    }

    public void RemoveFocus() {
        focus = null;
        hasInteracted = false;
        StopFollowingTarget();
        combat.StopAutoAttack();
    }

    public void FollowTarget(Interactable newTarget) {
        if (newTarget == null) {
            return;
        }
        target = newTarget.interactionTransform;
        agent.SetDestination(target.position);
        agent.stoppingDistance = newTarget.radius * .8f;

    }

    public void StopFollowingTarget() {
        target = null;
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
        agent.SetDestination(transform.position);
    }

    protected void FaceTarget() {
        if (target == null) {
            return;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected void OnTriggerEnter(Collider other) {
        if (focus != null || other.isTrigger) {
            return;
        }

        BaseCharacterController otherCharController = other.GetComponent<BaseCharacterController>();
        if (otherCharController != null && otherCharController.faction != faction) {
            Debug.Log("Collider" + other);
            Debug.Log(gameObject.name + ": I can see " + other.gameObject.name);
            SetFocus(other.gameObject.GetComponent<Interactable>());
        }
    }
}

public enum FactionsEnum { Good, Evil };