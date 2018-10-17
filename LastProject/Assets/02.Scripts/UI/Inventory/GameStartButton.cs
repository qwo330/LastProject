using UnityEngine;

public class GameStartButton : MonoBehaviour {

	public void OnClickGameStart()
    {
        StageManager.Instance.ChangeScene(SceneState.Field1);
    }
}
