using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerState
{
    public PlayerDeath(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float verticalAxis, float horizontalAxis, float currentSpeed) 
        : base(status, transformComponent, rigidbodyComponent, animatorComponent, attackBoxCollider, isInHome, verticalAxis, horizontalAxis, currentSpeed)
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
        animatorComponent.SetBool(PlayerAniTrigger.DEATH, triggerValue);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }
}