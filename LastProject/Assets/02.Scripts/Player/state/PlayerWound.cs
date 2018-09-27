using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWound : State
{
    public PlayerWound(Animator animator, Rigidbody rigidbody, bool isWound)
    {
        this.animatorComponent = animator;
        this.rigidbodyComponent = rigidbody;
        this.isWound = isWound;
    }

    public override void DoAction()
    {
        isWound = true;
        rigidbodyComponent.velocity = Vector3.zero;
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, true);
    }
}
