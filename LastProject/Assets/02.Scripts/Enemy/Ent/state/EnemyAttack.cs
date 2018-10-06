using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState 
{
    public EnemyAttack(Animator animator, GameObject targetPlayer, Transform transform)
    {
        this.animatorComponent = animator;
        this.targetPlayer = targetPlayer;
        this.transform = transform;
    }

    public override void DoAction()
    {
        transform.LookAt(targetPlayer.transform);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, true);
    }
}
