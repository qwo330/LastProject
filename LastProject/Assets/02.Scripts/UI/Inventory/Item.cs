using System.Collections;

[System.Serializable]
public struct ItemData : IComparer
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

    int IComparer.Compare(object _x, object _y)
    {
        ItemData x = (ItemData)_x;
        ItemData y = (ItemData)_y;

        return x.Time.CompareTo(y.Time);
    }
}

public interface IEquipable { }
public interface IEatable { }
public interface IElapsable
{
    void Elapse();
}

//public class EquipmentItem : Item, IEquipable
//{
//    public ItemData Item;

//    override public void Elapse()
//    {
//        Item.ElapseTime();
//        if (Item.Time < 0)
//        {
//            deleteItem();
//        }
//    }

//    void deleteItem()
//    {
//        Item = ItemList.Instance.ItemIndex[0];
//        GetComponent<Image>().sprite = null;
//    }
//}

//public class FoodItem : Item, IEatable
//{
//    public List<ItemData> Items = new List<ItemData>();

//    override public void Elapse()
//    {
//        for (int i = 0; i < Items.Count; i++)
//        {
//            Items[i].ElapseTime();
//            if (Items[i].Time < 0)
//            {
//                deleteItem(i);
//            }
//        }
//    }

//    void deleteItem(int i)
//    {
//        Items[i] = ItemList.Instance.ItemIndex[0];
//        if (Items.Count == 0) GetComponent<Image>().sprite = null;
//    }
//}

//public abstract class Item : MonoBehaviour, IElapsable
//{
//    public Sprite sprite;
//    abstract public void Elapse();
//    public void Use()
//    {
//        // TODO : 아이템 사용했을 때
//    }
//}