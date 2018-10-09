using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerState
{
    public PlayerDeath(Animator animatorComponent, Rigidbody rigidbodyComponent)
    {
        this.animatorComponent = animatorComponent;
        this.rigidbodyComponent = rigidbodyComponent;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.DEATH, true);
        rigidbodyComponent.velocity = Vector3.zero;
    }   
}