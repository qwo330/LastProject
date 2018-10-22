using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : EnemyState 
{
    public EnemyDeath(Animator animator, Rigidbody rigidbodyComponent, NavMeshAgent navMeshAgent)
    {
        this.animatorComponent = animator;
        this.rigidbodyComponent = rigidbodyComponent;
        this.navMeshAgent = navMeshAgent;
    }

    public override void DoAction()
    {
        rigidbodyComponent.isKinematic = true;
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        animatorComponent.SetTrigger(PlayerAniTrigger.DEATH);
    }
}
