using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBookSlot : MonoBehaviour {
    public delegate void OnSlotChanged();
    public OnSlotChanged onSlotChangedCallback;


    public UIButton SlotButton;

    MonstersInfo info;

    public void AddMonster(MonstersInfo info)
    {
        SlotButton.enabled = true;
        SlotButton.defaultColor = new Color(0, 0, 0, 1);
    }
}
