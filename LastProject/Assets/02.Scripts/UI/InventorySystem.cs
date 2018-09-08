using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 참고자료 : https://unity3d.com/kr/learn/tutorials/projects/adventure-game-tutorial/inventory
public class InventorySystem : MonoBehaviour {
    public GameObject InventorySlots, CraftSlots;

    [SerializeField]
    ItemSlot[,] inventorySlots;
    Sprite[,] inventorySprites;

    [SerializeField]
    ItemSlot[,] craftSlots;
    Sprite[,] craftSprites;
    //List<ItemSlot> slots = new List<ItemSlot>();

    void CreateInventory()
    {


        ItemSlot[,] inventorySlots = new ItemSlot[Defines.InventoryRow, Defines.InventoryColunm];
        Sprite[,] inventorySprites = new Sprite[Defines.InventoryRow, Defines.InventoryColunm];

        ItemSlot[,] craftSlots = new ItemSlot[3, 3];
        Sprite[,] craftSprites = new Sprite[3, 3];
    }

    /// <summary>
    /// 해당 슬롯에 아이템이 있으면 true 리턴
    /// </summary>
    public bool CheckExistItem(int row, int col)
    {
        if (inventorySlots[row, col].ItemCode != ItemCodes.Empty) return true;
        return false;
    }

    public void AddInventorySlot(ItemSlot item)
    {

    }

    public void AddCraftSlot(ItemSlot item)
    {

    }

    public void SwapItemPosition()
    {

    }
}
