using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : EnemyState 
{
    public EnemyMove(Transform transform, Transform targetTransform, Rigidbody rigidbody, Animator animator, NavMeshAgent navMeshAgent, bool isAttackAble, 
        int dropExp, int dropGold, GameTimer deadTimer, GameTimer attackTimer, RemoveEnemy_Delegate removeEnemy_Delegate, GiveItem_Delegate giveItem_Delegate) 
        : base(transform, targetTransform, rigidbody, animator, navMeshAgent, isAttackAble, dropExp, dropGold, deadTimer, attackTimer, removeEnemy_Delegate, giveItem_Delegate)
    {
        this.targetTransform = targetTransform;
        this.animator = animator;
        this.navMeshAgent = navMeshAgent;
    }

    public override void Update()
    {
        navMeshAgent.SetDestination(targetTransform.position);
        base.Update();
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        animator.SetBool(PlayerAniTrigger.ISRUNNING, triggerValue);
        navMeshAgent.isStopped = !triggerValue;
    }
}
