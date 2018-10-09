using UnityEngine;

using UnityEngine.SceneManagement;

public class ManagerCaller : MonoBehaviour {

    private void Awake()
    {
        TimerManager.Instance.Init();
        ObjectPool.Instance.Init();
        ItemList.Instance.Init();
        StageManager.Instance.Init();
        //        DataManager.Instanc
        //AudioManager.Instance.Init();
        
        SceneManager.LoadScene(SceneState.Field1.ToString());
    }
}
