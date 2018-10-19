using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState 
{
    public EnemyIdle(Animator animator, NavMeshAgent navMeshAgent, Transform transform)
    {
        this.animatorComponent = animator;
        this.navMeshAgent = navMeshAgent;
        this.transform = transform;
        this.currentSpeed = 0;
    }

    public override void DoAction()
    {
        //transform.LookAt(Vector3.forward);
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, false);
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetTrigger(PlayerAniTrigger.ISIDLE);

        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
}
