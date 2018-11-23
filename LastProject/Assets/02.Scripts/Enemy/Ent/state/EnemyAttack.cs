using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState 
{
    public EnemyAttack(Transform transform, Transform targetTransform, Rigidbody rigidbody, Animator animator, NavMeshAgent navMeshAgent, bool isAttackAble, 
        int dropExp, int dropGold, GameTimer deadTimer, GameTimer attackTimer, RemoveEnemy_Delegate removeEnemy_Delegate, GiveItem_Delegate giveItem_Delegate) 
        : base(transform, targetTransform, rigidbody, animator, navMeshAgent, isAttackAble, dropExp, dropGold, deadTimer, attackTimer, removeEnemy_Delegate, giveItem_Delegate)
    {
        this.transform = transform;
        this.targetTransform = targetTransform;
        this.animator = animator;
        this.navMeshAgent = navMeshAgent;
        this.isAttackAble = isAttackAble;
        this.dropExp = dropExp;
        this.dropGold = dropGold;
        this.attackTimer = attackTimer;
    }

    public override void Enter()
    {
        if (isAttackAble)
        {
            transform.LookAt(targetTransform);
            attackTimer.SetTimer(4f);
            attackTimer.StartTimer();
            Debug.Log("isAttackable : false");
            isAttackAble = false;
            base.Enter();
        }
        else
        {
            Exit();
        }
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        navMeshAgent.isStopped = triggerValue;
        animator.SetBool(PlayerAniTrigger.ATTACK, triggerValue);
    }
}
