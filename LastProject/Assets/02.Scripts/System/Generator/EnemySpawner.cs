using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int MaxCount;
    GameTimer spawnTimer;
    const float spawnTime = 2f;
    List<GameObject> enemyList;
    bool isRespawn;
    
    [SerializeField, Range(1, 10)]
    int RespawnLevel;

    const float MinRandomPos = -2f;
    const float MaxRandomPos = 2f;

    public void Init(int maxCount)
    {
        MaxCount = maxCount;
        spawnTimer = TimerManager.Instance.GetTimer();
        spawnTimer.SetTimer(spawnTime);
        spawnTimer.Callback = Spawn;
        enemyList = new List<GameObject>();
        isRespawn = false;
    }

    void Spawn()
    {
        if (enemyList.Count < MaxCount)
        {
            Vector3 randomPosition = 
                new Vector3(Random.Range(MinRandomPos, MaxRandomPos), 
                transform.position.y, Random.Range(MinRandomPos, MaxRandomPos));
            abstractEnemy enemy = ObjectPool.Instance.PopEnt(transform.position + randomPosition, RespawnLevel);
            enemy.RemoveEnemy_Delegate = RemoveAt;
            enemyList.Add(enemy.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Defines.TAG_Player)
        {
            isRespawn = true;
            StartCoroutine(respawn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Defines.TAG_Player)
        {
            isRespawn = false;
            StopCoroutine(respawn());
        }
    }

    IEnumerator respawn()
    {
        while (isRespawn)
        {
            yield return new WaitForSeconds(spawnTime);

            Spawn();
        }
    }

    void RemoveAt(GameObject enemy)
    {
        enemyList.Remove(enemy);
        enemy.GetComponent<abstractEnemy>().ReturnPool();
    }

    public void RemoveAll()
    {
        for (int i = enemyList.Count; i > 0; i--)
        {
            RemoveAt(enemyList[i]);
        }
    }
}