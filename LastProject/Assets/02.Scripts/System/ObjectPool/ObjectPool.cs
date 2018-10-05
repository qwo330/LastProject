using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    const int ENT_POOLCOUNT = 30;

    Player playerCharacter;
    List<Ent> EntList;

    public void Init()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Archu"), transform) as GameObject;
        playerCharacter = go.GetComponent<Player>();
        //test용 코드 data 작업 이후 삭제----------
        playerCharacter.Init(0,0,0); //플레이어 스텟 설정
        playerCharacter.MovingSpeed = 5;
        //-----------------------------------------
        playerCharacter.gameObject.SetActive(false);

        
    }

    public Player PopPlayer(Vector3 Position)
    {
        playerCharacter.gameObject.SetActive(true);
        playerCharacter.gameObject.transform.position = Position;

        return playerCharacter;
    }
}
