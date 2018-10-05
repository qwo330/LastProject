using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : EnemyState 
{
    const float chaseDistance = 4f;

    public EnemyMove(Animator animator, NavMeshAgent navMeshAgent, GameObject targetPlayer)
    {
        this.navMeshAgent = navMeshAgent;
        this.animatorComponent = animator;
        this.targetPlayer = targetPlayer;
    }

    public override void DoAction()
    {
        if(targetPlayer != null)
        {
            animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, true);
            navMeshAgent.SetDestination(targetPlayer.transform.position);
            navMeshAgent.isStopped = false;
        }
    }
}
