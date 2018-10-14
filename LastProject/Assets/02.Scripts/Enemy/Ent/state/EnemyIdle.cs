using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState 
{
    public EnemyIdle(Animator animator, NavMeshAgent navMeshAgent, Transform transform, Rigidbody rigidbody)
    {
        this.animatorComponent = animator;
        this.navMeshAgent = navMeshAgent;
        this.transform = transform;
        this.rigidbodyComponent = rigidbody;
    }

    public override void DoAction()
    {
        transform.LookAt(Vector3.forward);
        rigidbodyComponent.velocity = Vector3.zero;
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, false);
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetTrigger(PlayerAniTrigger.ISIDLE);
        navMeshAgent.isStopped = true;
    }
}
