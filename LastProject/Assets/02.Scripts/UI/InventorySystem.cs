using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 참고자료 : https://unity3d.com/kr/learn/tutorials/projects/adventure-game-tutorial/inventory
public class InventorySystem : MonoBehaviour {
    public GameObject InventoryPanel, CraftPanel;
    public GameObject SlotPrefab;

    List<GameObject> inventoryObjects = new List<GameObject>();
    List<ItemSlot> inventorySlots = new List<ItemSlot>();
    List<Image> iventoryImgs = new List<Image>();

    List<GameObject> craftObjects = new List<GameObject>();
    List<ItemSlot> craftSlots = new List<ItemSlot>();
    List<Image> craftImgs = new List<Image>();

    int shownSlotCount;
    int itemCount;

    private void Start()
    {
        CreateInventory();
    }

    void CreateInventory()
    {
        shownSlotCount = 12;
        itemCount = 0;
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            GameObject obj = Instantiate(SlotPrefab, InventoryPanel.transform);
            inventoryObjects.Add(obj);
            inventorySlots.Add(obj.GetComponent<ItemSlot>());
        }
    }

    /// <summary>
    /// 인벤토리에 보이는 라인 수를 늘린다.
    /// </summary>
    public void AddSlots()
    {
        if (shownSlotCount >= 30) return;

        RectTransform rt = InventoryPanel.GetComponent<RectTransform>();
        rt.offsetMin = new Vector2(0, rt.offsetMin.y - 60f);
        shownSlotCount += 6;
    }

    /// <summary>
    /// 인벤토리에 보이는 라인 수를 줄인다.
    /// </summary>
    public void RemoveSlots()
    {
        if (shownSlotCount <= 12) return;

        RectTransform rt = InventoryPanel.GetComponent<RectTransform>();
        rt.offsetMin = new Vector2(0, rt.offsetMin.y + 60f);
        shownSlotCount -= 6;
    }

    /// <summary>
    /// 해당 슬롯에 아이템이 있으면 true 리턴
    /// </summary>
    public bool CheckExistItem(int index)
    {
        if (inventorySlots[index].GetComponent<ItemSlot>().ItemCode != ItemCodes.Empty) return true;
        return false;
    }

    /// <summary>
    /// 인벤토리에서 비어있는 슬롯을 찾아 index 리턴
    /// </summary>
    public int FindEmptySlot()
    {
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            if (!CheckExistItem(i)) return i;
        }
        return -1;
    }

    /// <summary>
    /// 인벤토리에 해당 아이템이 존재하는지 확인하고 있다면 index, 없으면 -1 리턴
    /// </summary>
    public int FindItem(ItemCodes itemCode)
    {
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            if (itemCode == inventorySlots[i].GetComponent<ItemSlot>().ItemCode)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 아이템 획득 시 인벤토리에 추가
    /// </summary>
    public void AddInventorySlot(ItemSlot item)
    {
        int index = FindEmptySlot();
        //inventorySlots.Add(item);
    }

    /// <summary>
    /// 조합창으로 아이템을 옮겼을 때 추가
    /// </summary>
    public void AddCraftSlot(ItemSlot item)
    {

    }
}
