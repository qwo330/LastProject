using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : State
{
    public PlayerDeath(Animator animatorComponent, Rigidbody rigidbodyComponent, CharacterState playerStates)
    {
        this.animatorComponent = animatorComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.playerStates = playerStates;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.DEATH, playerStates == CharacterState.Death);
    }   
}
