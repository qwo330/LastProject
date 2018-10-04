using UnityEngine;

using UnityEngine.SceneManagement;

public class ManagerCaller : MonoBehaviour {

    private void Awake()
    {
        ObjectPool.Instance.Init();
        ItemList.Instance.Init();
        StageManager.Instance.Init();
        //        DataManager.Instanc
        //AudioManager.Instance.Init();
        TimerManager.Instance.Init();

        SceneManager.LoadScene(SceneState.Field1.ToString());
    }
}
