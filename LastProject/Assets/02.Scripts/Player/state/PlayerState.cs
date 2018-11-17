using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected CharacterStatus status;
    protected Transform transformComponent;
    protected Rigidbody rigidbodyComponent;
    protected Animator animatorComponent;
    protected PlayerAttackBox attackBoxCollider;
    protected bool isInHome;
    protected float VerticalAxis;
    protected float HorizontalAxis;
    protected float currentSpeed;

    public PlayerState(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float verticalAxis, float horizontalAxis, float currentSpeed)
    {
        this.status = status;
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.attackBoxCollider = attackBoxCollider;
        this.isInHome = isInHome;
        VerticalAxis = verticalAxis;
        HorizontalAxis = horizontalAxis;
        this.currentSpeed = currentSpeed;
    }

    public virtual void Enter()
    {
        PlayAnimation(true);
    }

    public virtual void Exit()
    {
        PlayAnimation(false);
    }

    protected abstract void PlayAnimation(bool triggerValue);
}