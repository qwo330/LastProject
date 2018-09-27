using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAniTrigger
{
    public const string ATTACK = "IsAttack";
    public const string ACTION = "SetAction";
    public const string WOUND = "IsWound";
    public const string DEATH = "IsDeath";
    public const string ISRUNNING = "IsRunnig";
    public const string ISINHOME = "InHome";
}

public class PlayerScr : MonoBehaviour
{
    bool isRunning;
    bool isInHome;
    bool isAttack;
    bool isDeath;
    bool isWound;

    public State CurrentState;

    CharacterStatus status;
    Rigidbody rigidbodyComponent;
    Animator animatorComponent;
    Collider attackBoxCollider;
    Collider hitBoxCollider;

    float VerticalAxis;
    float HorizontalAxis;
    bool isMouseClicked;

    [SerializeField, Range(0, 10)]
    float speed;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        attackBoxCollider = GetComponentInChildren<BoxCollider>();
        hitBoxCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        isRunning = false;
        isInHome = false;
        isAttack = false;
        isDeath = false;
        isWound = false;

        status = new CharacterStatus();
        SetMoveSpeed(speed);
        ChangeState(CharacterState.Idle);
        attackBoxCollider.enabled = false;
    }

    private void Update()
    {
        VerticalAxis = Input.GetAxis("Vertical");
        HorizontalAxis = Input.GetAxis("Horizontal");
        isMouseClicked = Input.GetMouseButtonDown(0);
    }

    private void FixedUpdate()
    {
        if(!isWound && !isDeath)
        {
            if (!isAttack)
            {
                //attack
                if (isMouseClicked)
                {
                    isRunning = false;
                    isAttack = true;
                    ChangeState(CharacterState.Attack);
                }
                
                //moving
                else if (VerticalAxis != 0 || HorizontalAxis != 0)
                {
                    isRunning = true;
                    ChangeState(CharacterState.Move);
                }

                //idle
                else
                {
                    isRunning = false;

                    if (CurrentState is PlayerMove)
                    {
                        ChangeState(CharacterState.Idle);
                    }
                }
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
                CurrentState = new PlayerAttack(animatorComponent, attackBoxCollider, rigidbodyComponent);
                break;
            case CharacterState.Damaged:
                CurrentState = new PlayerWound(animatorComponent, rigidbodyComponent, isWound);
                break;
            case CharacterState.Death:
                break;
            default:
                break;
        }
        CurrentState.DoAction();
    }

    void SetStateIdle()
    {
        ChangeState(CharacterState.Idle);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Defines.TAG_EnemyAttackBox)
        {
            ChangeState(CharacterState.Damaged);
            isRunning = false;
            isWound = true;
        }
    }

    void OnAttackExit()
    {
        attackBoxCollider.enabled = false;
        isAttack = false;
        isRunning = false;
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, isAttack);
        ChangeState(CharacterState.Idle);
    }

    void OnWoundExit()
    {
        isWound = false;
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, isWound);
        ChangeState(CharacterState.Idle);
    }
}