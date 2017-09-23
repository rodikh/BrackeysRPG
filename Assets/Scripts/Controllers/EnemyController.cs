using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseCharacterController {
    	
	// Update is called once per frame
	//void Update () {
 //       if (focus != null) {
 //           agent.SetDestination(focus.transform.position);
 //           FaceTarget();
 //       }


        //    if (distance <= agent.stoppingDistance) {
        //         Attack the target
        //        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        //        if (targetStats != null) {
        //            combat.Attack(targetStats);
        //        }
        //        FaceTarget();

        //    }
    //}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<BaseCharacterController>() != null) { 
            Debug.Log(gameObject.name + ": I can see " + other.gameObject.name);
            SetFocus(other.gameObject.GetComponent<Interactable>());
        }
    }
}
