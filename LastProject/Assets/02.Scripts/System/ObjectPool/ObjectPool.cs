using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    const int ENT_POOLCOUNT = 30;

    Player playerCharacter;
    //List<Ent> EntList;

    public void Init()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Archu")) as GameObject;
        playerCharacter = go.GetComponent<Player>();
        playerCharacter.Init();
        playerCharacter.gameObject.SetActive(false);
        playerCharacter.MovingSpeed = 5; //test용 코드, data 작업 이후 삭제
        playerCharacter.gameObject.transform.position = new Vector3(40, 1.5f, -11);//test용 코드, data 작업 이후 삭제
        //playerCharacter.SetStatus();
    }
}
