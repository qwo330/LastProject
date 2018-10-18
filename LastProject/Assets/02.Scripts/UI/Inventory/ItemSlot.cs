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

            if (Item[i].Time <= 0)
                deleteItem(i);
        }
    }

    void deleteItem(int p)
    {
        if(Item.Count != 0)
        {
            Item.RemoveAt(p);
            //inventorySystem.instance.SetCountText(this);
        }

        if (Item.Count <= 0)
        {
            GetComponent<Image>().sprite = ImageStorage.Instance.sprites[0];
        }
    }

    // 소모성 아이템을 사용 했을 때 그 효과를 실행하는 함수
    public void Use(int p)
    {
        deleteItem(p);

        CharacterStatus status = StageManager.Instance.player.status;
        status.cHealth += (Item[0].Value / 100) * status.MaxHealth;

        Debug.Log("회복량 : " + (Item[0].Value / 100) * status.MaxHealth);
    }
}

public class ItemSlot : Slot {
    public int Position;
}
