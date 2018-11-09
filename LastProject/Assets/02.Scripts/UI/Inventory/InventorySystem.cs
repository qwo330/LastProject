using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject InventoryPanel, EquipmentPanel;
    public GameObject SlotPrefab;

    public Image EmptyImg;
    public GraphicRaycaster gr;
    PointerEventData ped;

    public GameObject ItemPopUp;
    public Image ItemPopUpImg;
    Text itemPopUpText;

    public GameObject TradePopUp;
    Text tradePopUpText;
    Button confirmButton;

    public GameObject MessagePanel;
    Text messageText;

    //InventorySystem inventory;

    [SerializeField]
    EquipmentSlot[] equipmentSlots = new EquipmentSlot[4];

    List<ItemSlot> inventorySlots = new List<ItemSlot>();
    GameTimer itemTimer;

    bool isDrag = false;
    Vector3 prevPosition;
    Slot dragTarget;

    /*===========================================================*/
    Dictionary<ItemCodes, ItemData> itemlist;

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

    public void Init()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        EmptyImg.gameObject.SetActive(false);

        itemTimer = TimerManager.Instance.GetTimer();
        for (int i = 0; i < 4; i++)
            itemTimer.Callback += equipmentSlots[i].Elapse;

        itemTimer.SetTimer(1f, true);
        itemTimer.StartTimer();
        
        CreateInventory();

        itemPopUpText = ItemPopUp.GetComponentInChildren<Text>();
        ItemPopUp.SetActive(false);

        tradePopUpText = TradePopUp.GetComponentInChildren<Text>();
        TradePopUp.SetActive(false);

        messageText = MessagePanel.GetComponentInChildren<Text>();
        MessagePanel.SetActive(false);
    }

    void CreateInventory()
    {
        for (int i = 0; i < Defines.InventorySize; i++)
        {
            GameObject obj = Instantiate(SlotPrefab, InventoryPanel.transform);
            ItemSlot slot = obj.GetComponent<ItemSlot>();
            slot.Position = i;
            inventorySlots.Add(slot);

            if(itemTimer != null)
                itemTimer.Callback += slot.Elapse;
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        isDrag = true;
        prevPosition = Input.mousePosition;
        EmptyImg.transform.position = Input.mousePosition;
        dragTarget = getItemInfo();
    }

    public void OnDrag(PointerEventData data)
    {
        if(dragTarget == null || dragTarget.Item.Count == 0)
        {
            isDrag = false;
            EmptyImg.gameObject.SetActive(false);
            return;
        }

        float distance = Vector3.Distance(prevPosition, Input.mousePosition);

        if (EmptyImg.IsActive())
            EmptyImg.transform.position = Input.mousePosition;
        else if (distance > 15f)
        {
            int itemCode = (int)dragTarget.Item[0].ItemCode;
            Sprite sprite = UIPresenter.Instance.ImageStorage.sprites[itemCode];
            if (sprite == null) return;

            EmptyImg.sprite = sprite;
            EmptyImg.gameObject.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        EmptyImg.gameObject.SetActive(false);
        if (dragTarget == null || dragTarget.Item.Count == 0) return;

        Slot targetSlot = getItemInfo();
        if (targetSlot == null) // End 지점이 TradePanel 일때
        {        

            GameObject obj = getSaleItem();
            if (obj== null) return;

            TradePopUp.SetActive(true);
            tradePopUpText.text = dragTarget.Item[0].ItemName + "\n" + dragTarget.Item[0].Cost + "\n";
        }
        else // End 지점이 Item, EquipmentPanel 일때
        {
            if (targetSlot is ItemSlot) // 인벤토리의 slot
            {
                swapItem(targetSlot, dragTarget);
            }
            else if (targetSlot is EquipmentSlot) // 장비의 slot //장비창에서 등록처리
            {
                //TODO : 인벤토리와 장비 slot에서 아이템 교체
                ItemTypes part = targetSlot.GetComponent<EquipmentSlot>().Part;

                if (part == dragTarget.Item[0].ItemType)
                    swapItem(targetSlot, dragTarget);
                else
                    Debug.Log("올바르지 못한 착용 부위입니다.");
            }
        }
    }

    // 인벤토리의 아이템 클릭시 아이템에 대한 정보를 띄운다.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDrag) return;

        dragTarget = getItemInfo();
        if (dragTarget == null || dragTarget.Item.Count == 0) return;

        ItemData targetItem = dragTarget.Item[0];
        if (targetItem.ItemCode == ItemCodes.Empty) return;

        ItemPopUp.SetActive(true);
        itemPopUpText.text = targetItem.ItemName + "\n" + targetItem.Time.ToString();
        ItemPopUpImg.sprite = UIPresenter.Instance.ImageStorage.sprites[(int)targetItem.ItemCode];
    }

    Slot getItemInfo()
    {
        if (ped == null) return null;
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

    GameObject getSaleItem()
    {
        GameObject result = null;

        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        if (results.Count != 0)
        {
            if (results[0].gameObject.CompareTag("TradeUI"))
            {
                result = results[0].gameObject;
            }
        }
        return result;
    }

    // Slot의 데이터와 이미지 Swap.
    void swapItem(Slot targetItem, Slot dragTarget)
    {
        List<ItemData> tempItem = targetItem.Item;
        targetItem.Item = dragTarget.Item;
        dragTarget.Item = tempItem;

        Sprite targetSprite = targetItem.GetComponent<Image>().sprite;
        Sprite dragSprite = dragTarget.GetComponent<Image>().sprite;

        targetItem.GetComponent<Image>().sprite = dragSprite;
        dragTarget.GetComponent<Image>().sprite = targetSprite;

        SetCountText(targetItem); SetCountText(dragTarget);
    }

    /// <summary>
    /// 해당 슬롯에 아이템이 있으면 true 리턴
    /// </summary>
    bool CheckExistItem(int index)
    {
        if (inventorySlots[index].Item.Count == 0) return false;
        if (inventorySlots[index].Item[0].ItemCode != ItemCodes.Empty) return true;
        return false;
    }

    /// <summary>
    /// 인벤토리에서 비어있는 슬롯을 찾아 index 리턴
    /// </summary>
    int FindEmptySlot()
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
    /// 인벤토리의 index에 위치한 슬롯을 반환한다.
    /// </summary>
    public Slot GetTargetSlot(int index)
    {
        if (inventorySlots[index] == null) return null;
        return inventorySlots[index];
    }

    /// <summary>
    /// item의 소지 개수를 반환한다.
    /// </summary>
    public int GetItemCount(ItemData item)
    {
        int count = 0;

        int index = FindItemPosition(item);
        if (index == -1)
        {
            Debug.Log("해당 아이템이 존재하지 않습니다."); return count;
        }

        Slot slot = GetTargetSlot(index);
        count = slot.Item.Count;

        return count;
    }

    public void RemoveIteminInventory(ItemData item, int count)
    {
        int index = FindItemPosition(item);
        if (index == -1)
        {
            Debug.Log("해당 아이템이 존재하지 않습니다."); return;
        }

        Slot slot = GetTargetSlot(index);
        if (slot.Item.Count >= count)
        {
            for (int i = 0; i < count; i++)
                slot.Item.RemoveAt(0);
        }
    }

    /// <summary>
    /// 아이템 획득 시 인벤토리에 추가
    /// </summary>
    public void AddIteminInventory(ItemData item)
    {
        int index = FindItemPosition(item);
        if ((item.ItemType == ItemTypes.Eat || item.ItemType == ItemTypes.RawMaterial) 
            && index != -1)
        {
            inventorySlots[index].Item.Add(item);
            SetCountText(inventorySlots[index]);
            //inventorySlots[index].gameObject.GetComponent<Text>().text = (inventorySlots[index].Item.Count + 1).ToString();
            //inventorySlots[index].Item.Sort(); // 남은 Time 값에 맞춰 정렬 // 순서대로 add 되면 정렬 아닌가?
            return;
        }

        if (FindEmptySlot() == -1)
        {
            Debug.Log("가방이 가득 찼습니다. 더 이상 아이템을 획득할 수 없습니다.");
            return;
        }

        index = FindEmptySlot();
        inventorySlots[index].Item.Add(item);
        SetCountText(inventorySlots[index]);

        Sprite temp = null;
        temp = UIPresenter.Instance.ImageStorage.sprites[(int)item.ItemCode];
        inventorySlots[index].gameObject.GetComponent<Image>().sprite = temp;
    }

    void SetCountText(Slot target)
    {
        if (target is EquipmentSlot) return;

        if (target.Item.Count <= 1)
            target.GetComponentInChildren<Text>().text = string.Empty;
        else
            target.GetComponentInChildren<Text>().text = target.Item.Count.ToString();
    }

    public void OnClickConfirm()
    {
        Debug.Log("골드 획득 : " + dragTarget.Item[0].Cost);
        DataManager.Instance.SetPlayerData(); // 추후 구현
        TradePopUp.SetActive(false);
    }

    public void OnClickUse()
    {
        Debug.Log("사용");
        StartCoroutine(showUseMessage());
        ItemPopUp.SetActive(false);
    }

    IEnumerator showUseMessage()
    {
        if (dragTarget == null)// || dragTarget.Item.Count == 0)
        {
            MessagePanel.SetActive(false);
            yield break;
        }

        MessagePanel.SetActive(true);

        ItemTypes type = dragTarget.Item[0].ItemType;

        if (type == ItemTypes.RawMaterial)
        {
            messageText.text = "사용할 수 없는 아이템입니다.";
        }
        else if (type == ItemTypes.Eat)
        {
            messageText.text = "아이템을 사용하였습니다.";
            dragTarget.Use(0);
        }
        else // equipment
        {
            messageText.text = "장비를 교체하였습니다.";

            if (type == equipmentSlots[0].Part)
                swapItem(dragTarget, equipmentSlots[0]);

            else if (type == equipmentSlots[1].Part)
                swapItem(dragTarget, equipmentSlots[1]);

            else if (type == equipmentSlots[2].Part)
                swapItem(dragTarget, equipmentSlots[2]);

            else if (type == equipmentSlots[3].Part)
                swapItem(dragTarget, equipmentSlots[3]);
            else
                Debug.Log("잘못된 장비입니다.");
        }

        yield return new WaitForSeconds(0.7f);
        MessagePanel.SetActive(false);
    }

    // 내구도에 따른 아이템 경고
    public void WarnItemDelete()
    {
        // TODO : 아이템의 내구도가 x분 이하이면 경고 UI 표기 
        // (장비 / 음식 따로 표기 & 개수 표기)
    }
}