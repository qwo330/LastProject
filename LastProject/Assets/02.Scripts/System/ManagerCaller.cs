using UnityEngine;

using UnityEngine.SceneManagement;

public class ManagerCaller : MonoBehaviour {

    private void Awake()
    {
        DataManager.Instance.Init();
        ObjectPool.Instance.Init();
        ItemList.Instance.Init();
        StageManager.Instance.Init();
        TimerManager.Instance.Init();
        AudioManager.Instance.Init();

        SceneManager.LoadScene(SceneState.Field1.ToString());
    }
}
