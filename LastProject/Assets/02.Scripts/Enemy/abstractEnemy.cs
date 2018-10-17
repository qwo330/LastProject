using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 생성 직후 바로 SetStatus()로 스텟 설정해줘야 합니다.
/// </summary>
public abstract class abstractEnemy : MonoBehaviour
{
    public CharacterStatus status;
    public EnemyState currentState;

    public GameObject targetPlayer;
    protected EnemyAttackBox[] enemyAttackBox;
    protected CharacterState state;
    protected Rigidbody rigidbodyComponent;
    protected Animator animatorComponent;
    protected NavMeshAgent navMeshAgent;
    protected GameTimer deadTimer;
    protected GameTimer attackTimer;
    protected bool isAttackable;
    protected bool isDead;

    protected float MinChaseDistance = 2f;
    protected float MaxChaseDistance = 8f;
    protected float TargetDistance;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;

    public virtual abstractEnemy Init(int atk, int def, int hp, int lv)
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAttackBox = GetComponentsInChildren<EnemyAttackBox>();

        for (int i = 0; i < enemyAttackBox.Length; i++)
        {
            enemyAttackBox[i].enemy = this;
        }

        status = new CharacterStatus(atk, def, hp, lv);
        navMeshAgent.isStopped = true;
        isAttackable = true;
        isDead = false;

        currentState = null;
        targetPlayer = null;

        deadTimer = TimerManager.Instance.GetTimer();
        deadTimer.SetTimer(3f);
        deadTimer.Callback = DeadExit;
        attackTimer = TimerManager.Instance.GetTimer();
        attackTimer.SetTimer(2f);
        attackTimer.Callback = AttackTick;

        ObjectPool.Instance.allPushEnt += PushSelf;

        return this;
    }

    protected virtual void AttackTick()
    {
        isAttackable = true;
    }

    protected virtual void ChangeState(CharacterState state)
    {
        currentState.DoAction();
    }

    public abstract void PlayerWound(int damage);
    protected abstract void ONAttackExit();
    protected abstract void OnWoundExit();
    protected abstract void OnDeadExit();
    protected abstract void DeadExit();
    protected abstract void PushSelf();

    protected void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == Defines.TAG_Player)
        {
            targetPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == Defines.TAG_Player)
        {
            targetPlayer = null;
        }
    }
}