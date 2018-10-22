using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState 
{
    public EnemyAttack(Animator animator, GameObject targetPlayer, Transform transform, NavMeshAgent navMeshAgent, Rigidbody rigidbodyComponent)
    {
        this.animatorComponent = animator;
        this.targetPlayer = targetPlayer;
        this.transform = transform;
        this.navMeshAgent = navMeshAgent;
        this.rigidbodyComponent = rigidbodyComponent;
    }

    public override void DoAction()
    {
        rigidbodyComponent.isKinematic = true;
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
        transform.LookAt(targetPlayer.transform);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, true);
    }
}
