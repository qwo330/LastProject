using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : EnemyState 
{
    public EnemyDeath(Transform transform, Transform targetTransform, Rigidbody rigidbody, Animator animator, NavMeshAgent navMeshAgent, bool isAttackAble, 
        int dropExp, int dropGold, GameTimer deadTimer, GameTimer attackTimer, RemoveEnemy_Delegate removeEnemy_Delegate, GiveItem_Delegate giveItem_Delegate) 
        : base(transform, targetTransform, rigidbody, animator, navMeshAgent, isAttackAble, dropExp, dropGold, deadTimer, attackTimer, removeEnemy_Delegate, giveItem_Delegate)
    {
        this.transform = transform;
        this.animator = animator;
        this.navMeshAgent = navMeshAgent;
        this.dropExp = dropExp;
        this.dropGold = dropGold;
        this.deadTimer = deadTimer;
        this.RemoveEnemy_Delegate = removeEnemy_Delegate;
        this.giveItem_Delegate = giveItem_Delegate;
    }

    public override void Enter()
    {
        RemoveEnemy_Delegate(transform.gameObject);
        StageManager.Instance.player.GetExpAndGold(dropExp, dropGold);
        giveItem_Delegate();
        deadTimer.SetTimer(3f);
        deadTimer.StartTimer();
        base.Enter();
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        navMeshAgent.isStopped = triggerValue;
        animator.SetBool(PlayerAniTrigger.DEATH, triggerValue);
    }
}
