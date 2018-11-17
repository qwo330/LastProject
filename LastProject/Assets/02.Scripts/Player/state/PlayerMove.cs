using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    const float moveSpeedWeight = 10f;

    public PlayerMove(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float verticalAxis, float horizontalAxis, float currentSpeed) 
        : base(status, transformComponent, rigidbodyComponent, animatorComponent, attackBoxCollider, isInHome, verticalAxis, horizontalAxis, currentSpeed)
    {
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.isInHome = isInHome;
        this.currentSpeed = currentSpeed;
        this.VerticalAxis = verticalAxis;
        this.HorizontalAxis = horizontalAxis;
    }

    public override void Enter()
    {
        float traslation = VerticalAxis * currentSpeed;
        float rotation = HorizontalAxis * currentSpeed; 

        Vector3 v = new Vector3(rotation, 0, traslation);
        Quaternion q = Quaternion.identity;
        if (v != Vector3.zero) q = Quaternion.LookRotation(v);
        transformComponent.rotation = q;

        transformComponent.position = 
            new Vector3(transformComponent.position.x + rotation * TimerManager.Instance.DeltaTime, 
            transformComponent.position.y, transformComponent.position.z + traslation * TimerManager.Instance.DeltaTime);

        base.Enter();
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, triggerValue);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
        animatorComponent.SetTrigger(PlayerAniTrigger.ACTION);
    }
}
