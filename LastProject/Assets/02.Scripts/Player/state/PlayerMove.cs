using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    const float moveSpeedWeight = 10f;

    public PlayerMove(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float currentSpeed) 
        : base(status, transformComponent, rigidbodyComponent, animatorComponent, attackBoxCollider, isInHome, currentSpeed)
    {
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.isInHome = isInHome;
        this.currentSpeed = currentSpeed;
    }

    public override void Update()
    {
        base.Update();

        float traslation = InputManager.Instance.VerticalAxis * currentSpeed;
        float rotation = InputManager.Instance.HorizontalAxis * currentSpeed;

        Vector3 v = new Vector3(rotation, 0, traslation);
        Quaternion q = Quaternion.identity;
        if (v != Vector3.zero) q = Quaternion.LookRotation(v);
        transformComponent.rotation = q;

        transformComponent.position =
            new Vector3(transformComponent.position.x + rotation * TimerManager.Instance.DeltaTime,
            transformComponent.position.y, transformComponent.position.z + traslation * TimerManager.Instance.DeltaTime);
    }

    protected override void PlayAnimation(bool triggerValue)
    {
        animatorComponent.SetBool(PlayerAniTrigger.ISRUNNING, triggerValue);
        animatorComponent.SetBool(PlayerAniTrigger.ISINHOME, isInHome);
    }
}
