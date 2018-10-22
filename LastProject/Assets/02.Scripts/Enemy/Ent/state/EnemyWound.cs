using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWound : EnemyState 
{
    public EnemyWound(Animator animator, NavMeshAgent navMeshAgent, Rigidbody rigidbodyComponent)
    {
        this.animatorComponent = animator;
        this.navMeshAgent = navMeshAgent;
        this.rigidbodyComponent = rigidbodyComponent;
    }

    public override void DoAction()
    {
        rigidbodyComponent.isKinematic = true;
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, true);
    }
}
