using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void RemoveEnemy_Delegate(GameObject enemy);
public delegate void GiveItem_Delegate();
public abstract class abstractEnemy : MonoBehaviour
{
    protected GiveItem_Delegate GiveItem_Delegate;
    protected Transform targetTransform;
    protected EnemyAttackBox[] enemyAttackBox;
    protected EnemyState[] states;
    protected EnemyState previousState;
    protected EnemyState currentState;
    protected Rigidbody rigidbodyComponent;
    protected Animator animatorComponent;
    protected NavMeshAgent navMeshAgent;
    protected GameTimer deadTimer;
    protected GameTimer attackTimer;
    protected bool isAttackable;
    protected bool isDead;
    public RemoveEnemy_Delegate RemoveEnemy_Delegate;
    public CharacterStatus status;
    public int DropItemIndex;

    //상태
    protected const int IDLE = 0;
    protected const int MOVE = 1;
    protected const int ATTACK = 2;
    protected const int WOUNDED = 3;
    protected const int DEATH = 4;
    protected const int MAXCOUNT_STATE = 5;

    //ai 추적 거리
    protected const float MinChaseDistance = 2f;
    protected const float MaxChaseDistance = 8f;
    protected float TargetDistance;
    protected float chaseDistance;

    //스텟
    protected const int DefaultAtk = 50;
    protected const int DefaultDef = 10;
    protected const int DefaultHP = 500;
    protected const int IncreaseAtk = 10;
    protected const int IncreaseDef = 1;
    protected const int IncreaseHP = 50;
    protected int DropExp;
    protected int DropGold;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;

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

        currentState = null;
        previousState = null;
        targetTransform = null;

        deadTimer = TimerManager.Instance.GetTimer();
        deadTimer.SetTimer(3f);
        deadTimer.Callback = ReturnPool;

        attackTimer = TimerManager.Instance.GetTimer();
        attackTimer.SetTimer(2f);
        attackTimer.Callback = AttackTick;

        states = new EnemyState[MAXCOUNT_STATE];
        states[IDLE] = 
            new EnemyIdle(null, null, null, animatorComponent, null, false, 0, 0, null, null, null, null);

        states[MOVE] = 
            new EnemyMove(null, null, null, animatorComponent, navMeshAgent, false, 0, 0, null, null, null, null);

        states[ATTACK] = 
            new EnemyAttack(transform, targetTransform, rigidbodyComponent, animatorComponent, navMeshAgent, isAttackable, 0, 0, null, attackTimer, null, null);

        states[WOUNDED] = 
            new EnemyWound(null, null, rigidbodyComponent, animatorComponent, null, false, 0, 0, null, null, null, null);

        states[DEATH] = 
            new EnemyDeath(null, null, null, animatorComponent, navMeshAgent, false, DropGold, DropExp, deadTimer, null, RemoveEnemy_Delegate, GiveItem_Delegate);

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
            targetTransform = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == Defines.TAG_Player)
        {
            targetTransform = null;
        }
    }

    public void GiveItem()
    {
        UIPresenter.Instance.Inventory.AddIteminInventory(ItemList.Instance.ItemIndex[DropItemIndex + 51]);
        StageManager.Instance.player.GetExpAndGold(DropExp, DropGold);
    }

    public abstract void PlayerWound(int damage);
    public abstract void ReturnPool();
    protected abstract void ChangeState();
    protected abstract void ONAttackExit();
    protected abstract void OnWoundExit();
}