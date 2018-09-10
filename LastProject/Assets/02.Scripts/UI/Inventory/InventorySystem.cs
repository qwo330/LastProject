using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 참고자료 : https://unity3d.com/kr/learn/tutorials/projects/adventure-game-tutorial/inventory
public class InventorySystem : MonoBehaviour {
    public GameObject InventoryPanel, CraftPanel;
    public GameObject SlotPrefab;

    //List<GameObject> inventoryObjects = new List<GameObject>();
    List<ItemSlot> inventorySlots = new List<ItemSlot>();
    //List<Image> iventoryImgs = new List<Image>();

    //List<GameObject> craftObjects = new List<GameObject>();
    //List<ItemSlot> craftSlots = new List<ItemSlot>();
    //List<Image> craftImgs = new List<Image>();

    int shownSlotCount;
    int itemCount;

    /*===========================================================*/
    Dictionary<ItemCodes, ItemData> itemlist;

    private void Start()
    {
        //임시로 배치
        CreateInventory();
        //itemlist = new Dictionary<ItemCodes, ItemData>();
        //itemlist.Add(ItemCodes.TEMPSWORD, new TEMPSWORD());
        //itemlist.Add(ItemCodes.TEMPPOTION, new TEMPPOTION());
    }

    public void AddSword()
    {
        AddItem(new ItemData(ItemCodes.TEMPSWORD, 100, 100, false));
    }
    public void AddPotion()
    {
        AddItem(new ItemData(ItemCodes.TEMPPOTION, 100, 100, true));
    }
    
    void AddItem(ItemData item)
    {
        int index = FindItemPosition(item);
        if (item.IsStack && index != -1)
        {
            Debug.Log("중첩 가능한 아이템");
            // text에서 개수 증가
            //inventorySlots[index].gameObject.GetComponent<Text>().text = (inventorySlots[index].Item.Count + 1).ToString();
            return;
        }

        Debug.Log("중첩 불가능한 아이템");

        if (itemCount >= shownSlotCount)
            if (!AddSlot())
            {
                Debug.Log("가방이 가득 찼습니다. 더 이상 아이템을 획득할 수 없습니다.");
                return;
            }
            else Debug.Log("슬롯 확장");
        
        index = FindEmptySlot();
        inventorySlots[index].Item = item;
        inventorySlots[index].gameObject.GetComponent<Image>().sprite = ImageStorage.Instance.sprites[(int)item.ItemCode];
        itemCount++;
    }

    /* ===================================*/
    void CreateInventory()
    {
        shownSlotCount = 12;
        itemCount = 0;
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            GameObject obj = Instantiate(SlotPrefab, InventoryPanel.transform);
            ItemSlot slot = obj.GetComponent<ItemSlot>();
            slot.Position = i;
            inventorySlots.Add(slot);
        }
    }

    /// <summary>
    /// 인벤토리에 보이는 라인 수를 늘린다.
    /// </summary>
    public bool AddSlot()
    {
        if (shownSlotCount >= 30) return false;

        RectTransform rt = InventoryPanel.GetComponent<RectTransform>();
        rt.offsetMin = new Vector2(0, rt.offsetMin.y - 58f); // slotImageSize
        shownSlotCount += 6;
        return true;
    }

    /// <summary>
    /// 인벤토리에 보이는 라인 수를 줄인다.
    /// </summary>
    public void RemoveSlot()
    {
        if (shownSlotCount <= 12 && itemCount >= shownSlotCount) return;

        RectTransform rt = InventoryPanel.GetComponent<RectTransform>();
        rt.offsetMin = new Vector2(0, rt.offsetMin.y + 58f);
        shownSlotCount -= 6;
    }

    /// <summary>
    /// 해당 슬롯에 아이템이 있으면 true 리턴
    /// </summary>
    public bool CheckExistItem(int index)
    {
        if (inventorySlots[index].Item.ItemCode != ItemCodes.Empty) return true;
        return false;
    }

    /// <summary>
    /// 인벤토리에서 비어있는 슬롯을 찾아 index 리턴
    /// </summary>
    public int FindEmptySlot()
    {
        for (int i = 0; i < shownSlotCount; i++)
        {
            if (!CheckExistItem(i)) return i;
        }
        return -1;
    }

    /// <summary>
    /// 인벤토리에 해당 아이템이 존재하는지 확인하고 있다면 index, 없으면 -1 리턴
    /// </summary>
    public int FindItemPosition(ItemData item)
    {
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            if(item.ItemCode == inventorySlots[i].Item.ItemCode)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 아이템 획득 시 인벤토리에 추가
    /// </summary>
    public void AddIteminInventory(ItemData item)
    {
        int index = FindEmptySlot();
        //inventorySlots.Add(item);
    }

    /// <summary>
    /// 조합창으로 아이템을 옮겼을 때 추가
    /// </summary>
    public void AddIteminCraft(ItemData item)
    {

    }
}
