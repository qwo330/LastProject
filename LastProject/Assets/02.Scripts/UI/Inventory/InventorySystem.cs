using System.Collections;
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
    ItemSlot[] equipmentSlots = new ItemSlot[4]; // Weapon, Helmet, Armor, Shoes

    /*===========================================================*/
    Dictionary<ItemCodes, ItemData> itemlist;

    private void Start()
    {
        //임시로 배치
        CreateInventory();
    }

    public void AddSword()
    {
        AddIteminInventory(new ItemData(ItemCodes.TEMPSWORD, 100, 100, false));
    }
    public void AddPotion()
    {
        AddIteminInventory(new ItemData(ItemCodes.TEMPPOTION, 100, 100, true));
    }
    /* ===================================*/
    void CreateInventory()
    {
        ped = new PointerEventData(null);
        EmptyImg.gameObject.SetActive(false);

        ItemPopUpText = ItemPopUp.GetComponentInChildren<Text>();
        ItemPopUp.SetActive(false);

        for (int i = 0; i < Defines.InventorySize; i++)
        {
            GameObject obj = Instantiate(SlotPrefab, InventoryPanel.transform);
            ItemSlot slot = obj.GetComponent<ItemSlot>();
            slot.Position = i;
            inventorySlots.Add(slot);
        }
    }

    // 인벤토리의 아이템 클릭시 아이템에 대한 정보를 띄운다.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDrag) return;
        Debug.Log("Click");
        ItemData item;

        ItemSlot enter = getItemInfo();
        item = enter.Item;
        ItemPopUp.SetActive(true);
        ItemPopUpText.text = item.ItemName + "\n" + item.Time.ToString();
        ItemPopUpImg.sprite = ImageStorage.Instance.sprites[(int)item.ItemCode];
    }

    bool isDrag = false;
    Vector3 prevPosition;
    ItemSlot dragItemSlot;
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
            int itemCode = (int)dragItemSlot.Item.ItemCode;
            Sprite sprite = ImageStorage.Instance.sprites[itemCode];

            EmptyImg.sprite = sprite;
            EmptyImg.gameObject.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        ItemSlot enter = getItemInfo();
        if (enter != null) // itemslot이 존재하면 인벤토리의 slot
        {
            swapItem(enter);
            EmptyImg.gameObject.SetActive(false);
        }
        //else if // 장비의 slot //장비창에서 등록처리
        //{
        //    //AddIteminEquipment();
        //}
        EmptyImg.gameObject.SetActive(false);
    }

    ItemSlot getItemInfo()
    {
        ItemSlot result = null;

        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        if (results.Count != 0)
        {
            if (results[0].gameObject.CompareTag("ItemSlot"))
            {
                result = results[0].gameObject.GetComponent<ItemSlot>();
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
    void swapItem(ItemSlot target)
    {
        ItemData tempItem = target.Item;
        target.Item = dragItemSlot.Item;
        dragItemSlot.Item = tempItem;

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
        if (inventorySlots[index].Item.ItemCode != ItemCodes.Empty) return true;
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
            if (item.ItemCode == inventorySlots[i].Item.ItemCode)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 아이템 획득 시 인벤토리에 추가
    /// </summary>
    public void AddIteminInventory(ItemData item)
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
        if (FindEmptySlot() == -1)
        {
            Debug.Log("가방이 가득 찼습니다. 더 이상 아이템을 획득할 수 없습니다.");
            return;
        }
        index = FindEmptySlot();
        inventorySlots[index].Item = item;
        inventorySlots[index].gameObject.GetComponent<Image>().sprite = ImageStorage.Instance.sprites[(int)item.ItemCode];

    }
    /*====================================================*/
    /// <summary>
    /// 장비창으로 아이템을 옮겼을 때 추가
    /// </summary>
    public void AddIteminEquipment(ItemData item)
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
