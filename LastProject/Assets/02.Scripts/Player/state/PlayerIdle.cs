using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    public PlayerIdle(Animator animatorComponent, bool isRunning, bool isInHome)
    {
        this.animatorComponent = animatorComponent;
        this.isRunning = isRunning;
        this.isInHome = isInHome;
    }

    public override void DoAction()
    {
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, isRunning);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }    
}