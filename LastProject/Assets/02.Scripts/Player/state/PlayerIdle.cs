using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    public PlayerIdle(Animator animatorComponent, CharacterState playerStates, bool isInHome)
    {
        this.animatorComponent = animatorComponent;
        this.playerStates = playerStates;
        this.isInHome = isInHome;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, playerStates != CharacterState.Idle);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }    
}