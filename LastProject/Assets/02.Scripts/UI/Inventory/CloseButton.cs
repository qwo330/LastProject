using UnityEngine;

public class CloseButton : MonoBehaviour {
    public GameObject Panel;

    public void ClosePanel()
    {
        Panel.SetActive(false);
    }
}
