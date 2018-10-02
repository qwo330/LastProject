using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 생성 직후 바로 SetStatus()로 스텟 설정해줘야 합니다.
/// </summary>
public class Enemy : MonoBehaviour
{
    public CharacterStatus status;

    EnemyAttackBox enemyAttackBox;
    CharacterState state;
    Rigidbody rigidbodyComponent;
    Animator animatorComponent;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        enemyAttackBox = GetComponentInChildren<EnemyAttackBox>();
        enemyAttackBox.enemy = this;
        status = new CharacterStatus();
        enemyAttackBox.enabled = false;
    }

    public void PlayerWound(int damage)
    {

    }
    
    void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Running:
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Death:
                break;
            case CharacterState.Wound:
                break;
            default:
                break;
        }
    }









}
