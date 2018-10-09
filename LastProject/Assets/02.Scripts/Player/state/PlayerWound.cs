using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWound : PlayerState
{
    public PlayerWound(Animator animator, Rigidbody rigidbody)
    {
        this.animatorComponent = animator;
        this.rigidbodyComponent = rigidbody;
    }

    public override void DoAction()
    {
        rigidbodyComponent.velocity = Vector3.zero;
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, true);
    }
}
