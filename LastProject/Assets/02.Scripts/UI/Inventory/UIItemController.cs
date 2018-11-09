using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemController : MonoBehaviour
{
    public Image EmptyImg;
    public GraphicRaycaster gr;
    PointerEventData ped;

    public GameObject ItemPopUp;
    public Image ItemPopUpImg;
    Text ItemPopUpText;

    bool isDrag = false;
    Vector3 prevPosition;
    Slot dragItem;

    public void Init()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        EmptyImg.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        isDrag = true;
        prevPosition = Input.mousePosition;
        EmptyImg.transform.position = Input.mousePosition;
        dragItem = getItemInfo();
    }

    public void OnDrag(PointerEventData data)
    {
        if (dragItem.Item.Count == 0)
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
            int itemCode = (int)dragItem.Item[0].ItemCode;
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
        if (dragItem.Item.Count == 0) return;

        Slot targetItem = getItemInfo();
        if (targetItem is ItemSlot) // 인벤토리의 slot
        {
            swapItem(targetItem);
        }
        else if (targetItem is EquipmentSlot) // 장비의 slot //장비창에서 등록처리
        {
            //TODO : 인벤토리와 장비 slot에서 아이템 교체
            ItemTypes part = targetItem.GetComponent<EquipmentSlot>().Part;

            if (part == dragItem.Item[0].ItemType)
                swapItem(targetItem);
            else
                Debug.Log("올바르지 못한 착용 부위입니다.");
        }
    }

    // 인벤토리의 아이템 클릭시 아이템에 대한 정보를 띄운다.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDrag) return;

        Slot targetItem = getItemInfo();
        if (targetItem.Item.Count == 0) return;

        ItemData itemData = targetItem.Item[0];
        if (itemData.ItemCode == ItemCodes.Empty) return;

        ItemPopUp.SetActive(true);
        ItemPopUpText.text = itemData.ItemName + "\n" + itemData.Time.ToString();
        ItemPopUpImg.sprite = UIPresenter.Instance.ImageStorage.sprites[(int)itemData.ItemCode];
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

    // Slot의 데이터와 이미지 Swap.
    void swapItem(Slot targetItem)
    {
        List<ItemData> tempItem = targetItem.Item;
        targetItem.Item = dragItem.Item;
        dragItem.Item = tempItem;

        Sprite targetSprite = targetItem.GetComponent<Image>().sprite;
        Sprite dragSprite = dragItem.GetComponent<Image>().sprite;

        targetItem.GetComponent<Image>().sprite = dragSprite;
        dragItem.GetComponent<Image>().sprite = targetSprite;

        SetCountText(targetItem); SetCountText(dragItem);
    }

    void SetCountText(Slot target)
    {
        if (target is EquipmentSlot) return;

        if (target.Item.Count <= 1)
            target.GetComponentInChildren<Text>().text = string.Empty;
        else
            target.GetComponentInChildren<Text>().text = target.Item.Count.ToString();
    }
}