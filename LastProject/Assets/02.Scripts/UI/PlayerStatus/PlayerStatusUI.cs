using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour {
    
    private float HPValue;   //나중에 대원님한테 값 받아오기
    private float EXPValue;  //나중에 대원님한테 값 받아오기
    private int level;      //나중에 대원님한테 값 받아오기
    private int playerGold; //나중에 대원님한테 받아오기

    private float Percent = 0.01f;

    [SerializeField]
    private UISprite HPbar;
    [SerializeField]
    private UISprite EXPbar;
    [SerializeField]
    private UILabel playerLevel;
    [SerializeField]
    private UILabel goldObj;

    public void ChangeStatus()
    {
        playerLevel.text = level.ToString();
        HPbar.fillAmount = HPValue * Percent;
        EXPbar.fillAmount = EXPValue * Percent;
    }

    public void ShowPlayerGold()
    {
        goldObj.text = playerGold.ToString();
    }

}
