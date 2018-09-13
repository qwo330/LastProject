using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemData
{
    public ItemCodes ItemCode;
    public string ItemName;
    public int Time, MaxTime;
    public int Value; // 무기는 공격력, 방어구는 방어력, 포션은 회복력
    public bool IsStack;

    [SerializeField]
    public ItemData(ItemCodes itemCode, int maxDurability, int value, bool isStack)
    {
        ItemCode = itemCode;
        ItemName = itemCode.ToString();
        MaxTime = maxDurability;
        Time = MaxTime;
        Value = value;
        IsStack = isStack;
    }
}

public static class Defines{
    public const int InventoryRow = 5;
    public const int InventoryColunm = 6;
    public const int InventorySize = 30;
}
