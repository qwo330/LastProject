using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int MaxCount;
    GameTimer spawnTimer;
    const float spawnTime = 2f;
    List<Ent> entList;

    const float MinRandomPos = -2f;
    const float MaxRandomPos = 2f;

    public void Init(int maxCount)
    {
        MaxCount = maxCount;
        spawnTimer = TimerManager.Instance.GetTimer();
        spawnTimer.SetTimer(spawnTime);
        spawnTimer.Callback = Spawn;
        entList = new List<Ent>();
    }

    void Spawn()
    {
        if (entList.Count < MaxCount)
        {
            Vector3 randomPosition = new Vector3(Random.Range(MinRandomPos, MaxRandomPos), transform.position.y+2f, Random.Range(MinRandomPos, MaxRandomPos));
            Debug.Log(transform.position + randomPosition);
            entList.Add(ObjectPool.Instance.PopEnt(transform.position + randomPosition, 100, 10, 1, 500, entList));
            spawnTimer.SetTimer(spawnTime);
            spawnTimer.StartTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Defines.TAG_Player)
        {
            spawnTimer.SetTimer(spawnTime);
            spawnTimer.StartTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spawnTimer.StopTimer();
    }
}
