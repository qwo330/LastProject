using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState 
{
    public EnemyAttack(Animator animator)
    {
        this.animatorComponent = animator;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, true);
    }
}
