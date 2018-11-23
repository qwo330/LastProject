using UnityEngine;

public class ManagerCaller : MonoBehaviour
{
    private void Awake()
    {
        InputManager.Instance.Init();
        TimerManager.Instance.Init();
        ItemList.Instance.Init();
        ObjectPool.Instance.Init();
        StageManager.Instance.Init();
        DataManager.Instance.Init();
        UIPresenter.Instance.Init();
        //AudioManager.Instance.Init();
    }
}