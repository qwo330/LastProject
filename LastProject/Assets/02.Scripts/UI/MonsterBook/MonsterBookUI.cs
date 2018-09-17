using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBookUI : MonoBehaviour {

    public GameObject SlotParent;

    private int slotCount = 20;
    MonsterBookSlot monsterbookslot;

    MonsterBookSlot[] slots;

	void Start () {
        monsterbookslot.onSlotChangedCallback += UpdateUI;
        slots = SlotParent.GetComponentsInChildren<MonsterBookSlot>();
	}

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i<slotCount)
            {

            }
        }
    }

}
