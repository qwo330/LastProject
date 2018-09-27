using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : State
{
    public PlayerAttack(Animator animator, Collider attackBoxCollider, Rigidbody rigidbody)
    {
        this.animatorComponent = animator;
        this.attackBoxCollider = attackBoxCollider;
        this.rigidbodyComponent = rigidbody;   
    }

    public override void DoAction()
    {
        attackBoxCollider.enabled = true;
        rigidbodyComponent.velocity = Vector3.zero;
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, true);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }
}
