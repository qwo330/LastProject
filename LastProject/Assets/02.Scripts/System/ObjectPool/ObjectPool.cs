using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    const int ENT_POOLCOUNT = 30;

    Player playerCharacter;
    GameObject entPrefab;
    Queue<Ent> EntQueue;

    public void Init()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Archu"), transform) as GameObject;
        playerCharacter = go.GetComponent<Player>();
        //test용 코드 data 작업 이후 삭제----------
        playerCharacter.Init(0,0,0); //플레이어 스텟 설정
        playerCharacter.MovingSpeed = 5;
        //-----------------------------------------
        playerCharacter.gameObject.SetActive(false);

        EntQueue = new Queue<Ent>(ENT_POOLCOUNT);
        entPrefab = Resources.Load("Prefabs/Ent") as GameObject;
        for (int i = 0; i < ENT_POOLCOUNT; i++)
        {
            go = Instantiate(entPrefab, transform);
            EntQueue.Enqueue(go.GetComponent<Ent>());
            go.gameObject.SetActive(false);
        }
    }

    public Player PopPlayer(Vector3 Position)
    {
        playerCharacter.gameObject.SetActive(true);
        playerCharacter.gameObject.transform.position = Position;

        return playerCharacter;
    }

    public Ent PopEnt(Vector3 position, int atk, int def, int lv, int hp, List<Ent> list = null)
    {
        Ent ent = EntQueue.Dequeue();
        ent.gameObject.SetActive(true);
        ent.transform.position = position;
        ent.Init(atk, def, hp, lv);
        ent.MemberList = list;

        return ent;
    }

    public void PushEnt(Ent ent)
    {
        EntQueue.Enqueue(ent);
        ent.gameObject.SetActive(false);
    }
}
