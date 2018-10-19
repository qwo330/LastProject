using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    const float moveSpeedWeight = 10f;

    public PlayerMove(Transform transformComponent, float currentSpeed, 
        Rigidbody rigidbodyComponent, Animator animatorComponent, CharacterState playerState, 
        bool isInHome, float VerticalAxis, float HorizontalAxis)
    {
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.playerStates = playerState;
        this.isInHome = isInHome;
        this.currentSpeed = currentSpeed;
        this.VerticalAxis = VerticalAxis;
        this.HorizontalAxis = HorizontalAxis;
    }
    
    public override void DoAction()
    {
        float traslation = VerticalAxis * currentSpeed;
        float rotation = HorizontalAxis * currentSpeed; 

        Vector3 v = new Vector3(rotation, 0, traslation);
        Quaternion q = Quaternion.identity;
        if (v != Vector3.zero) q = Quaternion.LookRotation(v);
        transformComponent.position = 
            new Vector3(transformComponent.position.x + rotation * TimerManager.Instance.DeltaTime, 
            transformComponent.position.y, transformComponent.position.z + traslation * TimerManager.Instance.DeltaTime);
        transformComponent.rotation = q;

        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, playerStates == CharacterState.Running);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }
}
