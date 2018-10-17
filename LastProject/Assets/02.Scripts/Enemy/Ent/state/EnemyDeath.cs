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
        navMeshAgent.isStopped = true;
        rigidbodyComponent.velocity = Vector3.zero;
        animatorComponent.SetTrigger(PlayerAniTrigger.DEATH);
    }
}
