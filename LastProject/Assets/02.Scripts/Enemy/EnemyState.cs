using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    protected CharacterStatus status;
    protected EnemyAttackBox[] enemyAttackBox;
    protected CharacterState state;
    protected Rigidbody rigidbodyComponent;
    protected Animator animatorComponent;
    protected NavMeshAgent navMeshAgent;
    protected GameObject targetPlayer;
    protected Transform transform;

    public abstract void DoAction();
}
