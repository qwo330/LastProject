using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void AllPushEnt();

public class ObjectPool : Singleton<ObjectPool>
{
    const int ENT_POOLCOUNT = 30;

    Player playerCharacter;
    GameObject entPrefab;
    Queue<Ent> EntQueue;
    
    public AllPushEnt allPushEnt;

    public void Init()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Archu"), transform) as GameObject;
        playerCharacter = go.GetComponent<Player>();
        //test용 코드 data 작업 이후 삭제----------
        playerCharacter.Init(500,100,1000); //플레이어 스텟 설정
        playerCharacter.MovingSpeed = 5;
        //-----------------------------------------
        playerCharacter.gameObject.SetActive(false);

        //seed
        int seed = unchecked((int)DateTime.Now.Ticks);
        UnityEngine.Random.InitState(seed);
        Debug.Log("seed" + seed);
        
        //ent Queue setting
        EntQueue = new Queue<Ent>(ENT_POOLCOUNT);
        entPrefab = Resources.Load("Prefabs/Ent") as GameObject;
        for (int i = 0; i < ENT_POOLCOUNT; i++)
        {
            go = Instantiate(entPrefab, transform);
            Ent ent = go.GetComponent<Ent>();
            ent.DropItemIndex = UnityEngine.Random.Range(0, Defines.MaxDropItemCount);
            EntQueue.Enqueue(ent);
            go.gameObject.SetActive(false);
        }
    }

    public Player PopPlayer(Vector3 Position)
    {
        playerCharacter.gameObject.SetActive(true);
        playerCharacter.gameObject.transform.position = Position;

        return playerCharacter;
    }

    public Ent PopEnt(Vector3 position, int lv, List<Ent> list = null)
    {
        Ent ent = EntQueue.Dequeue();
        ent.gameObject.SetActive(true);
        ent.transform.position = position;
        ent.Init(lv);
        ent.MemberList = list;

        return ent;
    }

    public void PushEnt(Ent ent)
    {
        EntQueue.Enqueue(ent);
        ent.gameObject.SetActive(false);
    }
}
