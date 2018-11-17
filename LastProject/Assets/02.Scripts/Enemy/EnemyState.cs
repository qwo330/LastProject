using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    protected Transform transform;
    protected Transform targetTransform;
    protected Rigidbody rigidbody;
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    protected bool isAttackAble;
    protected int dropExp;
    protected int dropGold;
    protected GameTimer deadTimer;
    protected GameTimer attackTimer;
    protected RemoveEnemy_Delegate RemoveEnemy_Delegate;
    protected GiveItem_Delegate giveItem_Delegate;

    public EnemyState(Transform transform, Transform targetTransform, Rigidbody rigidbody, Animator animator, NavMeshAgent navMeshAgent, bool isAttackAble, 
        int dropExp, int dropGold, GameTimer deadTimer, GameTimer attackTimer, RemoveEnemy_Delegate removeEnemy_Delegate, GiveItem_Delegate giveItem_Delegate)
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

    public virtual void Enter()
    {
        PlayAnimation(true);
    }

    public virtual void Exit()
    {
        PlayAnimation(false);
    }

    protected abstract void PlayAnimation(bool triggerValue);
}
