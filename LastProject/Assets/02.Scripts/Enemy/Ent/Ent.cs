using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : abstractEnemy
{
    public List<Ent> MemberList;

    private void Update()
    {
        if (targetPlayer != null && !isDead)
        {
            TargetDistance = Vector3.Distance(targetPlayer.transform.position, transform.position);
            currentSpeed = MovingSpeed;

            if (!(currentState is EnemyDeath))
            {
                if (!(currentState is EnemyAttack && currentState is EnemyWound))
                {
                    if (TargetDistance < MaxChaseDistance)
                    {
                        if (TargetDistance > MinChaseDistance)
                        {
                            if (navMeshAgent.isStopped)
                            {
                                ChangeState(CharacterState.Running);
                            }
                        }
                        else
                        {
                            if ((currentState is EnemyIdle || currentState is EnemyMove))
                            {
                                if (isAttackable)
                                {
                                    navMeshAgent.isStopped = true;
                                    isAttackable = false;
                                    attackTimer.SetTimer(2f);
                                    attackTimer.StartTimer();
                                    ChangeState(CharacterState.Attack);
                                }
                            }
                        }
                    }
                    else
                    {
                        ChangeState(CharacterState.Idle);
                    }
                }
            }
        }
        else
        {
            ChangeState(CharacterState.Idle);
        }
    }

    protected override void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                currentState = new EnemyIdle(animatorComponent, navMeshAgent, transform);
                break;
            case CharacterState.Running:
                currentState = new EnemyMove(animatorComponent, navMeshAgent, targetPlayer, currentSpeed); 
                break;
            case CharacterState.Attack:
                currentState = new EnemyAttack(animatorComponent, targetPlayer, transform, navMeshAgent, rigidbodyComponent);
                break;
            case CharacterState.Death:
                currentState = new EnemyDeath(animatorComponent, rigidbodyComponent, navMeshAgent);
                break;
            case CharacterState.Wound:
                currentState = new EnemyWound(animatorComponent, navMeshAgent, rigidbodyComponent);
                break;
            default:
                break;
        }
        base.ChangeState(state); 
    }

    protected override void ONAttackExit()
    {
        rigidbodyComponent.isKinematic = false;
        ChangeState(CharacterState.Idle);
    }

    protected override void OnWoundExit()
    {
        rigidbodyComponent.isKinematic = false;
        ChangeState(CharacterState.Idle);
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
        animatorComponent.SetBool(PlayerAniTrigger.ATTACK, false);
    }

    public override void PlayerWound(int damage)
    {
        status.cHealth -= damage;

        if(status.cHealth <= 0 && !isDead)
        {
            if(MemberList != null && MemberList.Contains(this))
            {
                MemberList.Remove(this);
            }

            isDead = true;
            currentSpeed = 0;
            ChangeState(CharacterState.Death);
        }

        else
        {
            ChangeState(CharacterState.Wound);
        }
    }

    protected override void OnDeadExit()
    {
        deadTimer.SetTimer(3f);
        deadTimer.StartTimer();
    }

    protected override void DeadExit()
    {
        ObjectPool.Instance.PushEnt(this);
    }

    protected override void PushSelf()
    {
        ObjectPool.Instance.PushEnt(this);
    }
}