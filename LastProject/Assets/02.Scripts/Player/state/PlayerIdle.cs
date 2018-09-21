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

    public override void SetAnimation()
    {
        animatorComponent.SetBool("IsRunnig", isRunning);
        animatorComponent.SetBool("InHome", isInHome);
    }    
}