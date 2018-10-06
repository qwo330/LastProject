using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : EnemyState 
{
    public EnemyDeath(Animator animator)
    {
        this.animatorComponent = animator;
    }

    public override void DoAction()
    {
        animatorComponent.SetTrigger(PlayerAniTrigger.DEATH);
    }
}
