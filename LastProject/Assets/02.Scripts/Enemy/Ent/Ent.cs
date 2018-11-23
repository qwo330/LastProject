using UnityEngine;

public class Ent : abstractEnemy
{
    private void Update()
    {
        if(targetTransform != null)
            TargetDistance = Vector3.Distance(targetTransform.position, transform.position);
        navMeshAgent.speed = MovingSpeed;

        previousState = currentState;
        SetCurrentState();
        ChangeState();
    }

    void SetCurrentState()
    {
        if (targetTransform == null)
        {
            navMeshAgent.destination = transform.position;
            currentState = states[IDLE];
            return;
        }

        if (currentState == states[DEATH] || currentState == states[WOUNDED])
            return;

        if (currentState == states[ATTACK])
            return;

        if(TargetDistance < MaxChaseDistance)
        {
            if(MinChaseDistance < TargetDistance)
            {
                currentState = states[MOVE];
                currentState.targetTransform = targetTransform;
            }
            else
            {
                if (isAttackable)
                {
                    currentState = states[ATTACK];
                    currentState.isAttackAble = isAttackable;
                }
                else
                {
                    currentState = states[IDLE];
                }
            }
        }
        else
        {
            currentState = states[IDLE];
        }
    }
    
    protected override void ChangeState()
    {
        if (previousState == null || currentState == null)
            return;

        if (previousState == currentState)
        {
            currentState.Update();
        }
        else
        {
            previousState.Exit();
            currentState.Enter();
        }
    }

    public override void ReturnPool()
    {
        ObjectPool.Instance.PushEnt(this);
    }

    //이하 애니메이션 이벤트
    protected override void ONAttackExit()
    {
        SetPlayerState();
    }

    protected override void OnWoundExit()
    {
        SetPlayerState();
    }

    private void OnStartLeftAttack()
    {
        enemyAttackBox[0].colliderComponent.enabled = true;
    }

    private void OnEndLeftAttack()
    {
        enemyAttackBox[0].colliderComponent.enabled = false;
    }

    private void OnStartRightAttack()
    {
        enemyAttackBox[1].colliderComponent.enabled = true;
    }

    private void OnEndRightAttack()
    {
        enemyAttackBox[1].colliderComponent.enabled = false;
    }

    public override void PlayerWound(int damage)
    {
        if (currentState == states[DEATH])
            return;

        status.cHealth -= damage;

        if (status.cHealth <= 0)
        {
            previousState = currentState;
            currentState = states[DEATH];
            currentState.RemoveEnemy_Delegate = RemoveEnemy_Delegate;
            ChangeState();
        }
        else
        {
            SetPlayerState(states[WOUNDED]);
        }
    }

    public void SetPlayerState()
    {
        previousState = currentState;
        currentState = states[IDLE];
        ChangeState();
    }

    void SetPlayerState(EnemyState state)
    {
        previousState = currentState;
        currentState = state;
        ChangeState();
    }
}