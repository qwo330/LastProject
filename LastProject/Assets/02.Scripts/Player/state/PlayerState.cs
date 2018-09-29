using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterStatus
{
    public int Attack;
    public int Defense;
    public int Health;
    public float MovingSpeed;
}

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
    protected CharacterState playerStates;

    public abstract void DoAction();
}