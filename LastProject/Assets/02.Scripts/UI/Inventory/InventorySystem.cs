using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject InventoryPanel, CraftPanel;
    public GameObject SlotPrefab;

    public Image EmptyImg;
    public GraphicRaycaster gr;
    PointerEventData ped;

    public GameObject ItemPopUp;
    public Image ItemPopUpImg;
    Text ItemPopUpText;

    List<ItemSlot> inventorySlots = new List<ItemSlot>();
    EquipmentSlot[] equipmentSlots = new EquipmentSlot[4]; // Weapon, Helmet, Armor, Shoes
    GameTimer itemTimer;

    /*===========================================================*/
    Dictionary<ItemCodes, ItemData> itemlist;

    private void Start()
    {
        //임시로 배치
        CreateInventory();
    }

    public void AddWoodSword()
    {
        AddIteminInventory(new ItemData(ItemList.Instance.ItemIndex[1]));
    }

    public void AddStoneSword()
    {
        AddIteminInventory(new ItemData(ItemList.Instance.ItemIndex[2]));
    }
    public void AddPotion()
    {
        AddIteminInventory(new ItemData(ItemList.Instance.ItemIndex[41]));
    }
    /* ===================================*/
    void CreateInventory()
    {
        ped = new PointerEventData(null);
        EmptyImg.gameObject.SetActive(false);

        ItemPopUpText = ItemPopUp.GetComponentInChildren<Text>();
        ItemPopUp.SetActive(false);

        itemTimer = TimerManager.Instance.GetTimer();

        for (int i = 0; i < Defines.InventorySize; i++)
        {
            GameObject obj = Instantiate(SlotPrefab, InventoryPanel.transform);
            ItemSlot slot = obj.GetComponent<ItemSlot>();
            slot.Position = i;
            inventorySlots.Add(slot);

            itemTimer.Callback += slot.Elapse;
        }

        itemTimer.SetTimer();
        InvokeRepeating("StartTimer", 1f, 1f);
    }

    void StartTimer()
    {
        itemTimer.StartTimer();
    }

    bool isDrag = false;
    Vector3 prevPosition;
    Slot dragItemSlot;
    public void OnBeginDrag(PointerEventData data)
    {
        isDrag = true;
        prevPosition = Input.mousePosition;
        EmptyImg.transform.position = Input.mousePosition;
        dragItemSlot = getItemInfo();
    }

    public void OnDrag(PointerEventData data)
    {
        float distance = Vector3.Distance(prevPosition, Input.mousePosition);

        if (EmptyImg.IsActive())
            EmptyImg.transform.position = Input.mousePosition;
        else if (distance > 15f)
        {
            int itemCode = (int)dragItemSlot.Item[0].ItemCode;
            Sprite sprite = ImageStorage.Instance.sprites[itemCode];
            if (sprite == null) return;

            EmptyImg.sprite = sprite;
            EmptyImg.gameObject.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        Slot enter = getItemInfo();
        if (enter is ItemSlot) // 인벤토리의 slot
        {
            swapItem(enter);
        }
        else if(enter is EquipmentSlot) // 장비의 slot //장비창에서 등록처리
        {
            //TODO : 인벤토리와 장비 slot에서 아이템 교체
            ItemTypes part = enter.GetComponent<EquipmentSlot>().Part;
            Debug.Log("part : " + part.ToString());

            Debug.Log("dragItem : " + dragItemSlot.Item[0].ItemType.ToString());

            if (part == dragItemSlot.Item[0].ItemType)
                swapItem(enter);
            else
                Debug.Log("올바르지 못한 착용 부위입니다.");
        }
        EmptyImg.gameObject.SetActive(false);
    }

    // 인벤토리의 아이템 클릭시 아이템에 대한 정보를 띄운다.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDrag) return;

        ItemData itemData = getItemInfo().Item[0];
        if (itemData.ItemCode == ItemCodes.Empty) return;

        ItemPopUp.SetActive(true);
        ItemPopUpText.text = itemData.ItemName + "\n" + itemData.Time.ToString();
        ItemPopUpImg.sprite = ImageStorage.Instance.sprites[(int)itemData.ItemCode];
    }

    Slot getItemInfo()
    {
        Slot result = null;

        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        if (results.Count != 0)
        {
            if (results[0].gameObject.CompareTag("ItemSlot"))
            {
                result = results[0].gameObject.GetComponent<Slot>();
            }
        }
        return result;
    }

    //bool checkUsedSlot()
    //{
    //    ItemSlot enter = getItemInfo();
    //    if (enter == null)
    //    {
    //        Debug.Log("슬롯을 찾을 수 없습니다.");
    //        return false;
    //    }

    //    ItemCodes itemCode = enter.Item.ItemCode;
    //    if (itemCode != ItemCodes.Empty)
    //        return true;

    //    return false;
    //}

    // Slot의 데이터와 이미지 Swap.
    void swapItem(Slot target)
    {
        ItemData tempItem = target.Item[0];
        target.Item = dragItemSlot.Item;
        dragItemSlot.Item[0] = tempItem;

        Sprite targetSprite = target.GetComponent<Image>().sprite;
        Sprite dragSprite = dragItemSlot.GetComponent<Image>().sprite;

        target.GetComponent<Image>().sprite = dragSprite;
        dragItemSlot.GetComponent<Image>().sprite = targetSprite;
    }

    /// <summary>
    /// 해당 슬롯에 아이템이 있으면 true 리턴
    /// </summary>
    public bool CheckExistItem(int index)
    {
        if (inventorySlots[index].Item.Count == 0) return false;
        if (inventorySlots[index].Item[0].ItemCode != ItemCodes.Empty) return true;
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
    public int FindItemPosition(ItemData item)
    {
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            if (inventorySlots[i].Item.Count != 0
                && item.ItemCode == inventorySlots[i].Item[0].ItemCode)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 아이템 획득 시 인벤토리에 추가
    /// </summary>
    public void AddIteminInventory(ItemData item)
    {
        int index = FindItemPosition(item);
        if (item.ItemType == ItemTypes.Eat && index != -1)
        {
            Debug.Log("중첩 가능한 아이템");
            // text에서 개수 증가
            //inventorySlots[index].gameObject.GetComponent<Text>().text = (inventorySlots[index].Item.Count + 1).ToString();
            inventorySlots[index].Item.Add(item);
            //inventorySlots[index].Item.Sort(); // 남은 Time 값에 맞춰 정렬
            return;
        }

        Debug.Log("중첩 불가능한 아이템");
        if (FindEmptySlot() == -1)
        {
            Debug.Log("가방이 가득 찼습니다. 더 이상 아이템을 획득할 수 없습니다.");
            return;
        }
        index = FindEmptySlot();
        inventorySlots[index].Item.Add(item);
        inventorySlots[index].gameObject.GetComponent<Image>().sprite = ImageStorage.Instance.sprites[(int)item.ItemCode];

    }
    /*====================================================*/
    /// <summary>
    /// 장비창으로 아이템을 옮겼을 때 추가
    /// </summary>
    public void EquipItem(ItemData item)
    {

    }
}


/* 하정님 조합슬롯 추가,삭제에 참고
    ///// <summary>
    ///// 인벤토리에 보이는 라인 수를 늘린다.
    ///// </summary>
    //public bool AddSlot()
    //{
    //    if (shownSlotCount >= 30) return false;

    //    RectTransform rt = InventoryPanel.GetComponent<RectTransform>();
    //    rt.offsetMin = new Vector2(0, rt.offsetMin.y - 58f); // slotImageSize
    //    shownSlotCount += 6;
    //    return true;
    //}

    ///// <summary>
    ///// 인벤토리에 보이는 라인 수를 줄인다.
    ///// </summary>
    //public void RemoveSlot()
    //{
    //    if (shownSlotCount <= 12 && itemCount >= shownSlotCount) return;

    //    RectTransform rt = InventoryPanel.GetComponent<RectTransform>();
    //    rt.offsetMin = new Vector2(0, rt.offsetMin.y + 58f);
    //    shownSlotCount -= 6;
    //}
     */
