﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public delegate void AllPushEnt();
    public AllPushEnt allPushEnt;
    
    Player playerCharacter;
    GameObject entPrefab;
    Queue<abstractEnemy> EntQueue;

    const int ENT_POOLCOUNT = 30;
    const string PATH_PREFAB_ARCHU = "Prefabs/Archu";
    const string PATH_PREFAB_ENT = "Prefabs/Ent";

    public void Init()
    {
        GameObject go = Instantiate(Resources.Load(PATH_PREFAB_ARCHU), transform) as GameObject;
        playerCharacter = go.GetComponent<Player>();
        //test용 코드, data 작업 이후 수정/삭제----------
        int playerAtk = 500;
        int playerDef = 10;
        int playerHP = 1000;
        int playerSpeed = 5;
        //-----------------------------------------
        playerCharacter.Init(playerAtk, playerDef, playerHP);
        playerCharacter.MovingSpeed = playerSpeed;
        playerCharacter.gameObject.SetActive(false);

        //seed
        int seed = unchecked((int)DateTime.Now.Ticks);
        UnityEngine.Random.InitState(seed);
        Debug.Log("seed : " + seed);
        
        //ent Queue setting
        EntQueue = new Queue<abstractEnemy>(ENT_POOLCOUNT);
        entPrefab = Resources.Load(PATH_PREFAB_ENT) as GameObject;
        for (int i = 0; i < ENT_POOLCOUNT; i++)
        {
            go = Instantiate(entPrefab, transform);
            abstractEnemy ent = go.GetComponent<abstractEnemy>();
            ent.DropItemIndex = UnityEngine.Random.Range(0, Defines.MaxDropItemCount);
            EntQueue.Enqueue(ent);
            go.gameObject.SetActive(false);
        }
    }

    public Player PopPlayer(Vector3 Position)
    {
        playerCharacter.gameObject.SetActive(true);
        playerCharacter.gameObject.transform.position = Position;
        playerCharacter.SetPlayerState();

        return playerCharacter;
    }

    public abstractEnemy PopEnt(Vector3 position, int level)
    {
        abstractEnemy ent = EntQueue.Dequeue();
        ent.gameObject.SetActive(true);
        ent.transform.position = position;
        ent.Init(level);

        return ent;
    }

    public void PushEnt(abstractEnemy ent)
    {
        EntQueue.Enqueue(ent);
        ent.gameObject.SetActive(false);
    }
}