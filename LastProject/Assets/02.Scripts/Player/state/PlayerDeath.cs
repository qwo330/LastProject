using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerState
{
    bool isDead;

    public PlayerDeath(Animator animatorComponent, float currentSpeed, bool isDead)
    {
        this.animatorComponent = animatorComponent;
        this.currentSpeed = currentSpeed;
        this.isDead = isDead;
    }

    public override void DoAction()
    {
        currentSpeed = 0;
        animatorComponent.SetBool(PlayerAniTrigger.DEATH, true);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
        Debug.Log("dead");
    }   
}