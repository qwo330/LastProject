using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    public abstract void EnterAction(abstractEnemy enemy);
    public abstract void ExitAction(abstractEnemy enemy);
}
