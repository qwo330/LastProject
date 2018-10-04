using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] public InventorySystem InventorySystem;
    [SerializeField] UIGrid EpuipmentWindowGrid;
    [SerializeField] UIGrid PotionWindowGrid;
    [SerializeField] UIGrid NeedItemWindowGrid;
    [SerializeField] GameObject CraftSlot;
    [SerializeField] GameObject NeedSlot;
    CraftItemDB itemDB;
    public ItemData SelectedItem;

    int MaxEquipItemCount;
    int MaxPotionItemCount;
    const int MaxNeedItemCount = 4;

    List<CraftSlot> EquipList;
    List<CraftSlot> PotionList;
    List<NeedSlot> NeedList;

    public void Init()
    {
        MaxEquipItemCount = ItemList.Instance.EquipmentCount;
        MaxPotionItemCount = ItemList.Instance.FoodCount;

        Debug.Log(MaxEquipItemCount + ", " + MaxPotionItemCount);

        if (CraftSlot != null)
        {
            EquipList = CreateList<CraftSlot>(EpuipmentWindowGrid, MaxEquipItemCount, CraftSlot);
            PotionList = CreateList<CraftSlot>(PotionWindowGrid, MaxPotionItemCount, CraftSlot);
            NeedList = CreateList<NeedSlot>(NeedItemWindowGrid, MaxNeedItemCount, NeedSlot);
        }

        itemDB = new CraftItemDB();
        itemDB.Setting();

        int equipIndex = 0;
        int potionIndex = 0;

        foreach (CraftItem item in itemDB.CraftItems)
        {
            if (item.TargetItem.ItemType == ItemTypes.Eat)
            {
                PotionList[potionIndex].itemData = item.TargetItem;
                potionIndex++;
            }
            else if(item.TargetItem.ItemType == ItemTypes.Weapon
                || item.TargetItem.ItemType == ItemTypes.Helmet
                || item.TargetItem.ItemType == ItemTypes.Armor
                || item.TargetItem.ItemType == ItemTypes.Shoes)
            {
                EquipList[equipIndex].itemData = item.TargetItem;
                equipIndex++;
            }
        }
    }

    List<T> CreateList<T>(UIGrid windowGrid, int count, GameObject slot) where T: CraftSlotParent
    {
        List<T> list = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            GameObject item = NGUITools.AddChild(windowGrid.gameObject, slot);
            T itemComponent = item.GetComponent<T>();
            itemComponent.craftSystem = this;
            list.Add(itemComponent);
        }
        windowGrid.Reposition();

        return list;
    }

    public void ViewNeedItems(ItemData itemData)
    {
        int index = 0;

        foreach (CraftItem itemInDB in itemDB.CraftItems)
        {
            if(itemInDB.TargetItem.ItemCode == itemData.ItemCode)
            {
                for (int i = 0; i < itemInDB.NeedItems.Length; i++)
                {
                    if(itemInDB.NeedItems[i] > 0)
                    {
                        //인벤토리에서 보유 장비 개수도 읽어와서 같이 반영한다.
                        //InventorySystem.
                        NeedList[index].SetNeedCount(itemInDB.NeedItems[i]);
                        if (itemInDB.TargetItem.ItemType == ItemTypes.Eat)
                        {
                            NeedList[index].ChangeSprite((CraftItemResource)(i+100));
                        }
                        else if (itemInDB.TargetItem.ItemType == ItemTypes.Weapon
                            || itemInDB.TargetItem.ItemType == ItemTypes.Helmet
                            || itemInDB.TargetItem.ItemType == ItemTypes.Armor
                            || itemInDB.TargetItem.ItemType == ItemTypes.Shoes)
                        {
                            NeedList[index].ChangeSprite((CraftItemResource)i);
                        }
                        else
                        {
                            NeedList[index].ChangeSprite((CraftItemResource)999);
                        }
                        index++;
                    }
                }
                SelectedItem = itemInDB.TargetItem;
            }
        }

        //나머지 빈 슬롯 처리
        for (int i = index; i < MaxNeedItemCount; i++)
        {
            NeedList[i].ResetCount();
        }
    }

    public bool IsCrafty(out int slotEmptyCount)
    {
        slotEmptyCount = 0;

        foreach (NeedSlot slot in NeedList)
        {
            if (!slot.isCraft)
            {
                return false;
            }
            else if (slot.CountIsZero())
            {
                slotEmptyCount++;
            }
        }
        return true;
    }

    private void OnEnable()
    {
        Init();
    }
} 