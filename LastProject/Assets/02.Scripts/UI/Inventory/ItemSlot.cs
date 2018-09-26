using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    public List<ItemData> Item;

    public void Elapse()
    {
        if (Item.Count == 0) return;

        Debug.Log("아이템 내구도 감소");
        for (int i = 0; i < Item.Count; i++)
        {
            // List<T>.this[int i]에서 리턴시키는 임시변수에 수정 불가능
            ItemData tmp = Item[i];
            tmp.Time--;
            Item[i] = tmp;

            Debug.Log(i);
        }

        if (Item[0].Time < 0)
            deleteItem();
    }

    public void deleteItem()
    {
        if (Item.Count == 1)
        {
            Item[0] = ItemList.Instance.ItemIndex[0];
            GetComponent<Image>().sprite = null;
        }
        else
            Item.RemoveAt(0);
    }
}

public class ItemSlot : Slot {
    public int Position;
}
