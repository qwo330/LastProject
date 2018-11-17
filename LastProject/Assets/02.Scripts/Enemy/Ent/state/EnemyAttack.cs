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
        this.rigidbody = rigidbody;
        this.animator = animator;
        this.navMeshAgent = navMeshAgent;
        this.isAttackAble = isAttackAble;
        this.dropExp = dropExp;
        this.dropGold = dropGold;
        this.deadTimer = deadTimer;
        this.attackTimer = attackTimer;
        this.RemoveEnemy_Delegate = removeEnemy_Delegate;
        this.giveItem_Delegate = giveItem_Delegate;
    }

    public override void Enter()
    {
        if (isAttackAble)
        {
            navMeshAgent.isStopped = true;
            isAttackAble = false;
            transform.LookAt(targetTransform);
            attackTimer.SetTimer(2f);
            attackTimer.StartTimer();
            base.Enter();
        }
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        rigidbody.isKinematic = triggerValue;
        animator.SetBool(PlayerAniTrigger.ATTACK, triggerValue);
    }
}
