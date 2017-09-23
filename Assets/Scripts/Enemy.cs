using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start() {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override bool Interact(BaseCharacterController interactor) {
        return true;
        // TODO: interact using Character Combat and not here
        //base.Interact(interactor);
        //// Attack the enemy
        //CharacterCombat characterCombat = interactor.GetComponent<CharacterCombat>();

        //if (characterCombat != null) {
        //    characterCombat.Attack(myStats);
        //}

        ////Todo: true if dead
        //return false;
    }
}
