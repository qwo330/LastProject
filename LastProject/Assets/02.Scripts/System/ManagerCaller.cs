using UnityEngine;

public class ManagerCaller : MonoBehaviour {

    private void Awake()
    {
        TimerManager.Instance.Init();
        ObjectPool.Instance.Init();
        ItemList.Instance.Init();
        StageManager.Instance.Init();
        DataManager.Instance.Init();
        //AudioManager.Instance.Init();
    }
}
