using UnityEngine;
using UnityEngine.UI;

public interface IElapsable
{
    void Elapse();
}

public abstract class Slot : MonoBehaviour, IElapsable
{
    public ItemData[] Item;

    public void Elapse()
    {
        for (int i = 0; i < Item.Length; i++)
        {
            Item[i].Time--;
            if (Item[i].Time < 0)
            {
                deleteItem(i);
            }
        }  
    }

    void deleteItem(int i)
    {
        Item[i] = ItemList.Instance.ItemIndex[0];
        if(Item.Length == 0) GetComponent<Image>().sprite = null;
    }
}

public class ItemSlot : Slot {
    public int Position;
}