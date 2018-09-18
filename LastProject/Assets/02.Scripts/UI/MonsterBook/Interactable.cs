using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    MonstersInfo info;

    MonsterBookUI ui;


    private void Start()
    {
        ui.onSlotChangedCallback += Test;
    }

    void Test()
    {
        info.type.ToString();
    }
}
