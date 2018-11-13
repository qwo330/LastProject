﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct CharacterStatus
{
    public int Attack;
    public int Defense;
    public float cHealth;
    public float MaxHealth;
    public int Level;
    public float Exp;
    public float MaxExp;
    public int Gold;

    public CharacterStatus(int atk, int def, int hp)
    {
        Attack = atk;
        Defense = def;
        MaxHealth = hp;
        cHealth = MaxHealth;
        Level = 1;
        Exp = 0;
        MaxExp = 100 + Level * 200;
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
        MaxExp = 100 + Level * 200;
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
    DeathAndWound = Death & Wound,
    DeathXorWound = Death ^ Wound,
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
    bool isDead;

    float VerticalAxis;
    float HorizontalAxis;
    bool isMouseClicked;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;
    float currentSpeed;

    public Player Init(int atk, int def, int hp)
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        attackBoxCollider = GetComponentInChildren<PlayerAttackBox>();
        attackBoxCollider.player = this;
        attackBoxCollider.Collider.enabled = false;

        isInHome = false;
        status = new CharacterStatus(atk, def, hp);
        playerStates = CharacterState.Idle;
        ChangeState(playerStates);
        currentSpeed = MovingSpeed;
        GetExpAndGold(0,0);

        return this;
    }

    private void Update()
    {
        VerticalAxis = InputManager.Instance.VerticalAxis;
        HorizontalAxis = InputManager.Instance.HorizontalAxis;
        //if (!EventSystem.current.IsPointerOverGameObject())
        //if (UICamera.Raycast(Input.mousePosition))
#if UNITY_STANDALONE_WIN
        if (!InputManager.Instance.IsUIRaycast && !InputManager.Instance.IsPointOverUIObject)
        {
            isMouseClicked = InputManager.Instance.IsMouseClicked;
        }
#endif
    }

    private void FixedUpdate()
    {
        if((playerStates != CharacterState.Wound) && (playerStates != CharacterState.Death))
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
                else if ((VerticalAxis != 0 || HorizontalAxis != 0))
                {
                    playerStates = CharacterState.Running;
                    ChangeState(playerStates);
                }

                //idle
                else if(playerStates != CharacterState.Idle)
                {
                    transform.LookAt(transform);
                    playerStates = CharacterState.Idle;
                    ChangeState(playerStates);
                }

                currentSpeed = MovingSpeed;
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
                CurrentState = new PlayerMove(transform, currentSpeed, rigidbodyComponent, animatorComponent, playerStates, isInHome, VerticalAxis, HorizontalAxis);
                break;
            case CharacterState.Attack:
                CurrentState = new PlayerAttack(animatorComponent, playerStates, attackBoxCollider, rigidbodyComponent);
                break;
            case CharacterState.Wound:
                CurrentState = new PlayerWound(animatorComponent, currentSpeed);
                break;
            case CharacterState.Death:
                CurrentState = new PlayerDeath(animatorComponent, currentSpeed, isDead);
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
        if(playerStates == CharacterState.Death)
        {
            return;
        }

        currentSpeed = 0;
        status.cHealth -= damege;
        UIPresenter.Instance.DrawPlayerUI(status);

        if (status.cHealth < 0)
        {
            if (!isDead)
            {
                isDead = true;
                playerStates = CharacterState.Death;
            }
        }

        else
        {
            isDead = false;
            playerStates = CharacterState.Wound;
        }

        ChangeState(playerStates);
    }

    public void GetExpAndGold(int exp, int gold)
    {
        status.Exp += exp;
        status.Gold += gold;
        if (status.Exp > status.MaxExp)
        {
            status.Level++;
            status.Exp = 0;
        }
        status.MaxExp = 50 + status.Level * 100;
        UIPresenter.Instance.DrawPlayerUI(status);
    }
}