using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : State
{
    const float moveSpeedWeight = 10f;

    public PlayerMove(Transform transformComponent, float speed, Rigidbody rigidbodyComponent, Animator animatorComponent, CharacterState playerState, bool isInHome, float VerticalAxis, float HorizontalAxis)
    {
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.playerStates = playerState;
        this.isInHome = isInHome;
        this.status.MovingSpeed = speed;
        this.VerticalAxis = VerticalAxis;
        this.HorizontalAxis = HorizontalAxis;
    }
    
    public override void DoAction()
    {
        float traslation = VerticalAxis * status.MovingSpeed;
        float rotation = HorizontalAxis * status.MovingSpeed;
        Vector3 v = new Vector3(rotation, 0, traslation);
        Quaternion q = Quaternion.LookRotation(v);
        rigidbodyComponent.velocity = v;
        transformComponent.rotation = q;
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, playerStates == CharacterState.Running);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }
}
