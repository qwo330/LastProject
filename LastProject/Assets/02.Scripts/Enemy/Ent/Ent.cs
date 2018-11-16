using UnityEngine;

public class Ent : abstractEnemy
{
    private void Update()
    {
        TargetDistance = Vector3.Distance(targetPlayerTransform.position, transform.position);
        currentSpeed = MovingSpeed;

        SetCurrentState();
        ChangeState();
    }

    void SetCurrentState()
    {
        previousState = currentState;

        if (targetPlayerTransform == null)
            return;

        if (currentState == states[DEATH] || currentState == states[WOUNDED])
            return;

        if (currentState == states[ATTACK])
            return;

        if(TargetDistance < MaxChaseDistance)
        {
            if(MinChaseDistance < TargetDistance)
            {
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
        if (previousState == currentState)
            return;

        previousState.ExitAction(this);
        currentState.EnterAction(this);
    }

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
        currentState = currentState = states[IDLE];
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

    protected override void ReturnPool()
    {
        ObjectPool.Instance.PushEnt(this);
    }
}