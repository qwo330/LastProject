using UnityEngine;

public class PotalTrigger : MonoBehaviour {
    [SerializeField]
    SceneState next;

    private void OnTriggerEnter(Collider other)
    {
        StageManager.Instance.ChangeScene(next);
    }
}
