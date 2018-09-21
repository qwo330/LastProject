using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    public State CurrentState;

    CharacterStatus status;
    Rigidbody rigidbodyComponent;
    Animator animatorComponent;
    bool isRunning;
    bool isInHome;

    float VerticalAxis;
    float HorizontalAxis;

    [SerializeField, Range(0, 10)]
    float speed;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
    }

    private void Start()
    {
        isRunning = false;
        isInHome = false;
        status = new CharacterStatus();
        SetMoveSpeed(speed);
        ChangeState(CharacterState.Idle);
    }

    private void Update()
    {
        VerticalAxis = Input.GetAxis("Vertical");
        HorizontalAxis = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (VerticalAxis != 0 || HorizontalAxis != 0)
        {
            isRunning = true;
            ChangeState(CharacterState.Move);
        }
        else
        {
            isRunning = false;
            if(CurrentState is PlayerMove)
            {
                ChangeState(CharacterState.Idle);
            }
        }
    }

    public void SetStatus(int atk, int def, int hp)
    {
        status.Attack = atk;
        status.Defense = def;
        status.Health = hp;
    }

    public void SetMoveSpeed(float speed)
    {
        status.MovingSpeed = speed;
    }

    void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                CurrentState = new PlayerIdle(animatorComponent, isRunning, isInHome);
                break;
            case CharacterState.Move:
                CurrentState = new PlayerMove(transform, status.MovingSpeed, rigidbodyComponent, animatorComponent, isRunning, isInHome, VerticalAxis, HorizontalAxis);
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Damaged:
                break;
            case CharacterState.Death:
                break;
            default:
                break;
        }
        CurrentState.SetAnimation();
    }

    void SetStateIdle()
    {
        ChangeState(CharacterState.Idle);
    }
}