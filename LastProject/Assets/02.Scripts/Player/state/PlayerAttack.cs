using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    public PlayerAttack(Animator animator, CharacterState playerState, PlayerAttackBox attackBoxCollider, Rigidbody rigidbody)
    {
        this.animatorComponent = animator;
        this.playerStates = playerState;
        this.attackBoxCollider = attackBoxCollider;
        this.rigidbodyComponent = rigidbody;   
    }

    public override void DoAction()
    {
        attackBoxCollider.Collider.enabled = true;
        rigidbodyComponent.velocity = Vector3.zero;
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, playerStates == CharacterState.Attack);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }
}
