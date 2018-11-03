using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour {

	public void OnClickGameStart()
    {
        StageManager.Instance.ChangeScene(SceneState.Field1);
        GetComponent<Button>().interactable = false;
    }
}
