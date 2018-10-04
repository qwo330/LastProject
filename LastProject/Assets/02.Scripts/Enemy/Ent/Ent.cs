using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : abstractEnemy
{
    private void Update()
    {
        if(targetPlayer != null)
        {
            if(!(currentState is EnemyAttack && currentState is EnemyDeath && currentState is EnemyWound))
            {
                ChangeState(CharacterState.Running);
            }
        }
    }

    protected override void ChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Running:
                currentState = new EnemyMove(animatorComponent, navMeshAgent, targetPlayer); 
                break;
            case CharacterState.Attack:
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
        base.ONAttackExit();
    }

    protected override void OnWoundExit()
    {
        base.OnWoundExit();
    }

    private void Awake()
    {
        Init(0, 0, 0, 1);
    }
}
