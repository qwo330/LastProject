using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState 
{
    public EnemyIdle(Animator animator, NavMeshAgent navMeshAgent)
    {
        this.animatorComponent = animator;
        this.navMeshAgent = navMeshAgent;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, false);
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetTrigger(PlayerAniTrigger.ISIDLE);
        navMeshAgent.isStopped = true;
    }
}
