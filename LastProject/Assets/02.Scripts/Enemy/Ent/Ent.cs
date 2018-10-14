using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : abstractEnemy
{
    //private void Awake()
    //{
    //   Init(0, 0, 0, 1);
    //}

    public List<Ent> MemberList;

    private void Update()
    {
        if (targetPlayer != null)
        {
            TargetDistance = Vector3.Distance(targetPlayer.transform.position, transform.position);

            if (!(currentState is EnemyAttack || currentState is EnemyDeath || currentState is EnemyWound))
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
                currentState = new EnemyIdle(animatorComponent, navMeshAgent);
                break;
            case CharacterState.Running:
                currentState = new EnemyMove(animatorComponent, navMeshAgent, targetPlayer); 
                break;
            case CharacterState.Attack:
                currentState = new EnemyAttack(animatorComponent, targetPlayer, transform);
                break;
            case CharacterState.Death:
                currentState = new EnemyDeath(animatorComponent);
                break;
            case CharacterState.Wound:
                currentState = new EnemyWound(animatorComponent);
                break;
            default:
                break;
        }
        base.ChangeState(state); 
    }

    protected override void ONAttackExit()
    {
        ChangeState(CharacterState.Idle);
    }

    protected override void OnWoundExit()
    {
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
        if(status.cHealth <= 0)
        {
            if(MemberList != null && MemberList.Contains(this))
            {
                MemberList.Remove(this);
            }
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
}