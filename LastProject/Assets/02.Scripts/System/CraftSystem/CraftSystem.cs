using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] UIGrid EpuipmentWindowGrid;
    [SerializeField] UIGrid PotionWindowGrid;
    [SerializeField] UIGrid NeedItemWindowGrid;
    [SerializeField] GameObject CraftSlot;
    [SerializeField] GameObject NeedSlot;
    CraftItemDB itemDB;

    int MaxEquipItemCount = 3;
    int MaxPotionItemCount = 1;
    const int MaxNeedItemCount = 4;

    List<CraftSlot> EquipList;
    List<CraftSlot> PotionList;
    List<UISprite> NeedList;//스크립트 생기면 형식 변경

    private void Start()
    {
        //각 아이템 최대 개수를 얻어와야 함

        if (CraftSlot != null)
        {
            EquipList = CreateList<CraftSlot>(EpuipmentWindowGrid, MaxEquipItemCount, CraftSlot);
            PotionList = CreateList<CraftSlot>(PotionWindowGrid, MaxPotionItemCount, CraftSlot);
            NeedList = CreateList<UISprite>(NeedItemWindowGrid, MaxNeedItemCount, NeedSlot);
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

    List<T> CreateList<T>(UIGrid windowGrid, int count, GameObject slot)
    {
        List<T> list = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            GameObject item = NGUITools.AddChild(windowGrid.gameObject, slot);

            list.Add(item.GetComponent<T>());
        }
        windowGrid.Reposition();

        return list;
    }
}