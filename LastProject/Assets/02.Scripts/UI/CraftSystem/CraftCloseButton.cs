using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftCloseButton : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    void OnClick()
    {
        panel.SetActive(false);
    }
}
