using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCaller : MonoBehaviour {

    private void Awake()
    {
        ItemList.Instance.Init();
        StageManager.Instance.Init();
        ObjectPool.Instance.Init();
        //        DataManager.Instanc
        //AudioManager.Instance.Init();
        TimerManager.Instance.Init();
    }
}
