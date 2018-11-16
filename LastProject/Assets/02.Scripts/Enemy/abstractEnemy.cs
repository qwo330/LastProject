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
    public delegate void RemoveEnemy_Delegate(abstractEnemy enemy);
    public RemoveEnemy_Delegate RemoveEnemy;

    public CharacterStatus status;
    public Transform targetPlayerTransform;
    protected EnemyAttackBox[] enemyAttackBox;
    protected EnemyState[] states = new EnemyState[MAXCOUNT_STATE];
    protected EnemyState previousState;
    protected EnemyState currentState;
    public Rigidbody rigidbodyComponent;
    public Animator animatorComponent;
    public NavMeshAgent navMeshAgent;
    public GameTimer deadTimer;
    public GameTimer attackTimer;
    public bool isAttackable;
    public bool isDead;
    public int DropItemIndex;

    //적 캐릭터 상태
    protected const int IDLE = 0;
    protected const int MOVE = 1;
    protected const int ATTACK = 2;
    protected const int WOUNDED = 3;
    protected const int DEATH = 4;
    protected const int MAXCOUNT_STATE = 5;

    //ai 추적 거리
    protected float MinChaseDistance = 2f;
    protected float MaxChaseDistance = 8f;
    protected float TargetDistance;
    public float chaseDistance;

    //스텟
    protected const int DefaultAtk = 50;
    protected const int DefaultDef = 10;
    protected const int DefaultHP = 500;
    protected const int IncreaseAtk = 10;
    protected const int IncreaseDef = 1;
    protected const int IncreaseHP = 50;
    public int DropExp;
    public int DropGold;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;
    public float currentSpeed;

    public virtual abstractEnemy Init(int lv)
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAttackBox = GetComponentsInChildren<EnemyAttackBox>();

        for (int i = 0; i < enemyAttackBox.Length; i++)
        {
            enemyAttackBox[i].enemy = this;
        }

        status = new CharacterStatus(
            DefaultAtk + IncreaseAtk * (lv - 1), 
            DefaultDef + IncreaseDef * (lv - 1), 
            DefaultHP + IncreaseHP * (lv - 1), 
            lv);

        DropExp = 20 + lv * 10;
        DropGold = lv * 5;

        rigidbodyComponent.isKinematic = false;
        navMeshAgent.isStopped = true;
        isAttackable = true;
        isDead = false;

        currentSpeed = MovingSpeed;
        currentState = null;
        previousState = null;
        targetPlayerTransform = null;

        states[IDLE] = new EnemyIdle();
        states[MOVE] = new EnemyMove();
        states[ATTACK] = new EnemyAttack();
        states[WOUNDED] = new EnemyWound();
        states[DEATH] = new EnemyDeath();

        deadTimer = TimerManager.Instance.GetTimer();
        deadTimer.SetTimer(3f);
        deadTimer.Callback = ReturnPool;

        attackTimer = TimerManager.Instance.GetTimer();
        attackTimer.SetTimer(2f);
        attackTimer.Callback = AttackTick;

        return this;
    }

    protected virtual void AttackTick()
    {
        isAttackable = true;
    }

    
    protected void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == Defines.TAG_Player)
        {
            targetPlayerTransform = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == Defines.TAG_Player)
        {
            targetPlayerTransform = null;
        }
    }

    public void GiveItem()
    {
        UIPresenter.Instance.Inventory.AddIteminInventory(ItemList.Instance.ItemIndex[DropItemIndex + 51]);
        StageManager.Instance.player.GetExpAndGold(DropExp, DropGold);
    }

    public abstract void PlayerWound(int damage);
    protected abstract void ChangeState();
    protected abstract void ONAttackExit();
    protected abstract void OnWoundExit();
    protected abstract void ReturnPool();
}