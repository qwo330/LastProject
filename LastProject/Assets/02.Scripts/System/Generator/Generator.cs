using System.Collections;
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
    }

    public void GatherItem(int index, ItemCodes item)
    {
        gatherSpawner[index].gameObject.SetActive(false);
        StartCoroutine(Respawn(index, Defines.GatherRespawnTime));
        Debug.Log("GatherItem : " + item);
        UIPresenter.Instance.Inventory.AddIteminInventory(ItemList.Instance.ItemIndex[(int)item]);
    }

    IEnumerator Respawn(int index, float time)
    {
        yield return new WaitForSeconds(time); 

        gatherSpawner[index].gameObject.SetActive(true);
    }
}
