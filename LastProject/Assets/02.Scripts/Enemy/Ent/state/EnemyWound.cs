using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWound : EnemyState 
{
    public EnemyWound(Animator animator)
    {
        this.animatorComponent = animator;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, false);
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, true);
    }
}
