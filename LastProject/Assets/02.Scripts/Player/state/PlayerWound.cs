using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWound : PlayerState
{
    public PlayerWound(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float currentSpeed) 
        : base(status, transformComponent, rigidbodyComponent, animatorComponent, attackBoxCollider, isInHome, currentSpeed)
    {
        this.animatorComponent = animatorComponent;
        this.currentSpeed = currentSpeed;
    }

    public override void Enter()
    {
        currentSpeed = 0;
        base.Enter();
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        animatorComponent.SetBool(PlayerAniTrigger.WOUNDED, triggerValue);
    }
}
