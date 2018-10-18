using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : EnemyState 
{
    public EnemyDeath(Animator animator, Rigidbody rigidbodyComponent, NavMeshAgent navMeshAgent, float currentSpeed)
    {
        this.animatorComponent = animator;
        this.rigidbodyComponent = rigidbodyComponent;
        this.navMeshAgent = navMeshAgent;
        this.currentSpeed = currentSpeed;
    }

    public override void DoAction()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = currentSpeed;
        animatorComponent.SetTrigger(PlayerAniTrigger.DEATH);
    }
}
