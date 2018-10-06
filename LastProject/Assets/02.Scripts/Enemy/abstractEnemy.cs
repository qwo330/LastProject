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
    protected GameTimer attackTimer;

    protected float MinChaseDistance = 2f;
    protected float MaxChaseDistance = 8f;
    protected float TargetDistance;
    //protected Vector3 startPosition;
    //protected float startPositionDistance;

    [SerializeField, Range(0, 10)]
    public float MovingSpeed;

    private void Awake()
    {
        Init(0, 0, 0, 1);
    }

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
        attackTimer = TimerManager.Instance.GetTimer();
        attackTimer.SetTimer();
        attackTimer.Callback = EnemyUpdate;
        //attackTimer.
        attackTimer.StartTimer();
        return this;
    }

    protected abstract void EnemyUpdate();

    public void PlayerWound(int damage)
    {

    }

    protected virtual void ChangeState(CharacterState state)
    {
        currentState.DoAction();
    }

    protected virtual void ONAttackExit()
    {

    }

    protected virtual void OnWoundExit()
    {

    }

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