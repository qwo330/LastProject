using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOff : MonoBehaviour {

    [SerializeField]
    private GameObject activePanel;
    [SerializeField]
    private GameObject closeButton;


    public void OnClickMonsterButton()
    {
        activePanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void OnClickCloseButton()
    {
        activePanel.SetActive(false);
        closeButton.SetActive(false);
    }
}
