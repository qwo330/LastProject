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

public enum CharacterState
{
    Idle,
    Move,
    Attack,
    Damaged,
    Death,
}

public abstract class State
{
    protected CharacterStatus status;
    protected Transform transformComponent;
    protected Rigidbody rigidbodyComponent;
    protected Animator animatorComponent;
    protected bool isRunning;
    protected bool isInHome;
    protected float VerticalAxis;
    protected float HorizontalAxis;

    public abstract void SetAnimation();
}