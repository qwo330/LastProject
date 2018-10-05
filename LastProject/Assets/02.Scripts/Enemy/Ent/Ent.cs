using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : abstractEnemy
{
    private void Awake()
    {
        Init(0, 0, 0, 1);
    }

    protected override void EnemyUpdate()
    {
        if (targetPlayer != null)
        {
            TargetDistance = Vector3.Distance(targetPlayer.transform.position, transform.position);

            Debug.Log(currentState);
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
                            navMeshAgent.isStopped = true;
                            ChangeState(CharacterState.Attack);
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

        attackTimer.SetTimer(0.5f);
        attackTimer.StartTimer();
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
                currentState = new EnemyAttack(animatorComponent);
                break;
            case CharacterState.Death:
                break;
            case CharacterState.Wound:
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
        base.OnWoundExit();
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
}