using System;
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

        for (int i = 0; i < gatherSpawner.Length; i++)
        {
            gatherSpawner[i].Init(i);
        }

        ObjectPool.Instance.allPushEnt = new AllPushEnt(PushEntAll);
    }

    public void GatherItem(int index, ItemCodes item)
    {
        gatherSpawner[index].gameObject.SetActive(false);
        StartCoroutine(Respawn(index, Defines.GatherRespawnTime));
        UIPresenter.Instance.Inventory.AddIteminInventory(ItemList.Instance.ItemIndex[(int)item]);
    }

    IEnumerator Respawn(int index, float time)
    {
        yield return new WaitForSeconds(time); 

        gatherSpawner[index].gameObject.SetActive(true);
    }

    void PushEntAll()
    {
        for (int i = 0; i < enemySpawner.Length; i++)
        {
            enemySpawner[i].RemoveAll();
        }
    }
}
