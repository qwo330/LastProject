using UnityEngine;

public class PotalTrigger : MonoBehaviour {
    public SceneState next;

    private void OnTriggerEnter(Collider other)
    {
        StageManager.Instance.ChangeScene(next);
    }
}
