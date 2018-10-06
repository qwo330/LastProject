using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    
    EnemySpawner[] enemySpawner;
    Gatherspawner[] gatherSpawner;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        enemySpawner = GetComponentsInChildren<EnemySpawner>();
        gatherSpawner = GetComponentsInChildren<Gatherspawner>();

        foreach (EnemySpawner spawner in enemySpawner)
        {
            spawner.Init(Defines.TotalMonsterCount);
        }
    }
}
