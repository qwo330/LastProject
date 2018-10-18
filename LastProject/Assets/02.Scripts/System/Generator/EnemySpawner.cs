using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int MaxCount;
    GameTimer spawnTimer;
    const float spawnTime = 2f;
    List<Ent> entList;
    bool isRespawn;
    [SerializeField, Range(1, 100)] int RespawnLevel;

    const float MinRandomPos = 0f;
    const float MaxRandomPos = 0f;

    public void Init(int maxCount)
    {
        MaxCount = maxCount;
        spawnTimer = TimerManager.Instance.GetTimer();
        spawnTimer.SetTimer(spawnTime);
        spawnTimer.Callback= Spawn;
        entList = new List<Ent>();
        isRespawn = false;
    }

    void Spawn()
    {
        if (entList.Count < MaxCount)
        {
            Vector3 randomPosition = new Vector3(Random.Range(MinRandomPos, MaxRandomPos), transform.position.y, Random.Range(MinRandomPos, MaxRandomPos));
<<<<<<< HEAD
<<<<<<< HEAD
            entList.Add(ObjectPool.Instance.PopEnt(transform.position + randomPosition, 100, 10, 1, 500, entList));
            Debug.Log(name.ToString() + "  :  "  + transform.position);
=======
            entList.Add(ObjectPool.Instance.PopEnt(transform.position + randomPosition, RespawnLevel, entList));
>>>>>>> 26e58d00b08b0ed678fc9a2bd36e3c1054848006
=======
            entList.Add(ObjectPool.Instance.PopEnt(transform.position + randomPosition, RespawnLevel, entList));
>>>>>>> 26e58d00b08b0ed678fc9a2bd36e3c1054848006
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
}
