using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float currentSpeed) 
        : base(status, transformComponent, rigidbodyComponent, animatorComponent, attackBoxCollider, isInHome, currentSpeed)
    {
        this.animatorComponent = animatorComponent;
        this.isInHome = isInHome;
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        animatorComponent.SetBool(PlayerAniTrigger.ISIDLE, triggerValue);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
    }
}