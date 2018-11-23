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
    public bool isInHome;
    public float currentSpeed;

    public PlayerState(CharacterStatus status, Transform transformComponent, Rigidbody rigidbodyComponent, Animator animatorComponent, 
        PlayerAttackBox attackBoxCollider, bool isInHome, float currentSpeed)
    {
        this.status = status;
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.attackBoxCollider = attackBoxCollider;
        this.isInHome = isInHome;
        this.currentSpeed = currentSpeed;
    }

    public virtual void Enter()
    {
        PlayAnimation(true);
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        PlayAnimation(false);
    }

    protected abstract void PlayAnimation(bool triggerValue);
}