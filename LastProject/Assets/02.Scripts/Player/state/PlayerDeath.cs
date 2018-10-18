using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerState
{
    public PlayerDeath(Animator animatorComponent, float currentSpeed)
    {
        this.animatorComponent = animatorComponent;
        this.currentSpeed = currentSpeed;
    }

    public override void DoAction()
    {
        currentSpeed = 0;
        animatorComponent.SetBool(PlayerAniTrigger.DEATH, true);
    }   
}