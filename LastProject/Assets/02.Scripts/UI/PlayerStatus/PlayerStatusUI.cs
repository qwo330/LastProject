using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    private float Percent = 0.01f;

    [SerializeField]
    private UISprite HPbar;
    [SerializeField]
    private UISprite EXPbar;
    [SerializeField]
    private UILabel playerLevel;
    [SerializeField]
    private UILabel goldObj;

    public void ChangeStatus(CharacterStatus status)
    {
        playerLevel.text = "Lv." + status.Level.ToString("00") + " 앗츄";
        HPbar.fillAmount = status.cHealth / status.MaxHealth;
        EXPbar.fillAmount = status.Exp / status.MaxExp;
        goldObj.text = status.Gold.ToString();
    }
}
