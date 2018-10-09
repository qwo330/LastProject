using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct CharacterStatus
{
    public int Attack;
    public int Defense;
    public int cHealth;
    public int MaxHealth;
    public int Level;
    public int Exp;
    public int Gold;

    public CharacterStatus(int atk, int def, int hp)
    {
        Attack = atk;
        Defense = def;
        MaxHealth = hp;
        cHealth = MaxHealth;
        Level = 1;
        Exp = 0;
        Gold = 0;
    }

    public CharacterStatus(int atk, int def, int hp, int Lv)
    {
        Attack = atk;
        Defense = def;
        MaxHealth = hp;
        cHealth = MaxHealth;
        Level = Lv;
        Exp = 0;
        Gold = 0;
    }
}

public static class PlayerAniTrigger
{
    public const string ATTACK = "IsAttack";
    public const string ACTION = "SetAction";
    public const string WOUND = "IsWound";
    public const string DEATH = "IsDeath";
    public const string ISRUNNING = "IsRunnig";
    public const string ISINHOME = "InHome";
    public const string ISIDLE = "IsIdle";
}

[Flags]
public enum CharacterState
{
    Idle = 0,
    Running = 2,
    Attack = 4,
    Death = 8,
    Wound = 16,
    DeathOrWound = Death | Wound,
    DeathAndWound = Death & Wound
}

/// <summary>
/// SetStatus()으로 스텟 설정 꼭 해야함,
/// 마을 안과 밖일때 isInHome 변수 꼭 조정해줘야함
/// </summary>
public class Player : MonoBehaviour
{
    public PlayerState CurrentState;
    public bool isInHome;
    public CharacterStatus status;

    CharacterState playerStates;
    Rigidbody rigidbodyComponent;
    Animator animatorComponent;
    PlayerAttackBox attackBoxCollider;

    float VerticalAxis;
    float HorizontalAxis;
    bool isMouseClicked;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;

    public Player Init(int atk, int def, int hp)
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        attackBoxCollider = GetComponentInChildren<PlayerAttackBox>();
        attackBoxCollider.player = this;

        isInHome = false;
        status = new CharacterStatus(atk, def, hp);
        playerStates = CharacterState.Idle;
        ChangeState(playerStates);
        attackBoxCollider.enabled = false;

        return this;
    }

    private void Update()
    {
        VerticalAxis = Input.GetAxis("Vertical");
        HorizontalAxis = Input.GetAxis("Horizontal");
        //if (!EventSystem.current.IsPointerOverGameObject())
        //if (UICamera.Raycast(Input.mousePosition))
        if (!UICamera.Raycast(Input.mousePosition) && !EventSystem.current.IsPointerOverGameObject())
        {
            isMouseClicked = Input.GetMouseButtonDown(0);
        }
    }

    private void FixedUpdate()
    {
        if((playerStates != CharacterState.DeathOrWound))
        {
            if (playerStates != CharacterState.Attack)
            {
                //attack
                if (isMouseClicked && !isInHome)
                {
                    playerStates = CharacterState.Attack;
                    ChangeState(playerStates);
                }
                
                //moving
                else if (VerticalAxis != 0 || HorizontalAxis != 0)
                {
                    playerStates = CharacterState.Running;
                    ChangeState(playerStates);
                }

                //idle
                else if(playerStates != CharacterState.Idle)
                {
                    playerStates = CharacterState.Idle;
                    ChangeState(playerStates);
                }
            }  
        }
    }
    
    void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                CurrentState = new PlayerIdle(animatorComponent, playerStates, isInHome);
                break;
            case CharacterState.Running:
                CurrentState = new PlayerMove(transform, MovingSpeed, rigidbodyComponent, animatorComponent, playerStates, isInHome, VerticalAxis, HorizontalAxis);
                break;
            case CharacterState.Attack:
                CurrentState = new PlayerAttack(animatorComponent, playerStates, attackBoxCollider, rigidbodyComponent);
                break;
            case CharacterState.Wound:
                CurrentState = new PlayerWound(animatorComponent, rigidbodyComponent);
                break;
            case CharacterState.Death:
                CurrentState = new PlayerDeath(animatorComponent, rigidbodyComponent);
                break;
            default:
                break;
        }
        CurrentState.DoAction();
    }

    void OnAttackExit()
    {
        playerStates = CharacterState.Idle;
        attackBoxCollider.Collider.enabled = false;
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, playerStates == CharacterState.Attack);
        ChangeState(CharacterState.Idle);
    }

    void OnWoundExit()
    {
        playerStates = CharacterState.Idle;
        animatorComponent.SetBool(PlayerAniTrigger.WOUND, playerStates == CharacterState.Wound);
        ChangeState(CharacterState.Idle);
    }

    public void PlayerWound(int damege)
    {
        //status.cHealth -= damege;
        if(status.cHealth < 0)
        {
            playerStates = CharacterState.Death;  
        }
        else
        {
            playerStates = CharacterState.Wound;
        }
        ChangeState(playerStates);
    }
}