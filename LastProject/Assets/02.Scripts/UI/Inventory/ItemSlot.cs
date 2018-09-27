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

            //Debug.Log(i);
        }

        if (Item[0].Time <= 0)
            deleteItem();
    }

    void deleteItem()
    {
        for (int i = 0; i < Item.Count; i++)
        {
            if(Item[i].Time <= 0) Item.RemoveAt(i);
            //inventorySystem.instance.SetCountText(this);
        }

        if (Item.Count <= 0)
        {
            GetComponent<Image>().sprite = ImageStorage.Instance.sprites[0];
        }
    }
}

public class ItemSlot : Slot {
    public int Position;
}
