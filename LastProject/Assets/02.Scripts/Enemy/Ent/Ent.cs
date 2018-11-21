using UnityEngine;

public class Ent : abstractEnemy
{
    private void Update()
    {
        if(targetTransform != null)
            TargetDistance = Vector3.Distance(targetTransform.position, transform.position);
        navMeshAgent.speed = MovingSpeed;

        SetCurrentState();
        ChangeState();
    }

    void SetCurrentState()
    {
        previousState = currentState;

        if (targetTransform == null)
            return;

        if (currentState == states[DEATH] || currentState == states[WOUNDED])
            return;

        if (currentState == states[ATTACK])
            return;

        if(TargetDistance < MaxChaseDistance)
        {
            if(MinChaseDistance < TargetDistance)
            {
                currentState.targetTransform = targetTransform;
                currentState = states[MOVE];
            }
            else
            {
                currentState = states[ATTACK];
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
            return;

        previousState.Exit();
        currentState.Enter();
    }

    public override void ReturnPool()
    {
        ObjectPool.Instance.PushEnt(this);
    }

    //이하 애니메이션 이벤트
    protected override void ONAttackExit()
    {
        rigidbodyComponent.isKinematic = false;
        currentState = currentState = states[IDLE];
    }

    protected override void OnWoundExit()
    {
        rigidbodyComponent.isKinematic = false;
        currentState = currentState = states[IDLE];
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
        previousState = currentState;
        currentState = states[IDLE];
        previousState.Exit();
        currentState.Enter();
    }

    public override void PlayerWound(int damage)
    {
        status.cHealth -= damage;

        if (status.cHealth <= 0 && !isDead)
        {
            currentState = currentState = states[DEATH];
        }
        else
        {
            currentState = currentState = states[WOUNDED];
        }
    }
}