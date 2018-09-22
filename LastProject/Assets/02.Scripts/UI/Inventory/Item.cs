using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public ItemCodes ItemCode;
    public ItemTypes ItemType;
    public string ItemName;
    public int Time, MaxTime;
    public int Value; // 무기는 공격력, 방어구는 방어력, 포션은 회복력

    public ItemData(ItemCodes itemCode, ItemTypes itemType, int maxDurability, int value)
    {
        ItemCode = itemCode;
        ItemType = itemType;
        ItemName = itemCode.ToString();
        MaxTime = maxDurability;
        Time = MaxTime;
        Value = value;
    }

    public ItemData(ItemData proto)
    {
        ItemCode = proto.ItemCode;
        ItemType = proto.ItemType;
        ItemName = proto.ItemName;
        MaxTime = proto.MaxTime;
        Time = MaxTime;
        Value = proto.Value;
    }
}

//public interface IEquipable { }
//public interface IEatable { }

//public abstract class Item : MonoBehaviour
//{
//    public ItemData ItemData;
//    public Sprite sprite;
//}