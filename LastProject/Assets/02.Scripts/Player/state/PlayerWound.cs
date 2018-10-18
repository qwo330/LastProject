using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWound : PlayerState
{
    public PlayerWound(Animator animator, float currentSpeed)
    {
        this.animatorComponent = animator;
        this.currentSpeed = currentSpeed;
    }

    public override void DoAction()
    {
        currentSpeed = 0;
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, true);
    }
}
