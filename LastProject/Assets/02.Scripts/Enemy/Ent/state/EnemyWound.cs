using UnityEngine;
using UnityEngine.AI;

public class EnemyWound : EnemyState 
{
    public EnemyWound(Transform transform, Transform targetTransform, Rigidbody rigidbody, Animator animator, NavMeshAgent navMeshAgent, bool isAttackAble, 
        int dropExp, int dropGold, GameTimer deadTimer, GameTimer attackTimer, RemoveEnemy_Delegate removeEnemy_Delegate, GiveItem_Delegate giveItem_Delegate) 
        : base(transform, targetTransform, rigidbody, animator, navMeshAgent, isAttackAble, dropExp, dropGold, deadTimer, attackTimer, removeEnemy_Delegate, giveItem_Delegate)
    {
        this.rigidbody = rigidbody;
        this.animator = animator;
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        rigidbody.isKinematic = triggerValue;
        animator.SetBool(PlayerAniTrigger.WOUNDED, triggerValue);
    }
}
