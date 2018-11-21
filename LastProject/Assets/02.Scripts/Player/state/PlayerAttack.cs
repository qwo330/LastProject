using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    public PlayerAttack(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float currentSpeed) 
        : base(status, transformComponent, rigidbodyComponent, animatorComponent, attackBoxCollider, isInHome, currentSpeed)
    {
        this.animatorComponent = animatorComponent;
        this.attackBoxCollider = attackBoxCollider;
        this.rigidbodyComponent = rigidbodyComponent;
    }

    public override void Enter()
    {
        rigidbodyComponent.velocity = Vector3.zero;
        base.Enter();
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        attackBoxCollider.Collider.enabled = triggerValue;
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, triggerValue);
    }
}
