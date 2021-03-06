﻿using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

//제작 재료 순서대로
//장비 : 나무, 돌, 철, 아다만티움, 미스릴, 천, 양모, 가죽
//물약 : 물, 사과
public enum CraftItemResource
{
    Wood = 0,
    Stone,
    Iron,
    Adamantium,
    Mithrill,
    Fabric,
    Wool,
    Leather,

    Water = 100,
    Apple,

    Empty = 999,
}

public struct CraftItem
{
    public ItemData TargetItem;
    public int[] NeedItems;

    public CraftItem SetItemDB(ItemData item, int wood, int stone, int iron,
        int adamantium, int mithrill, int fabric, int wool, int leather)
    {
        NeedItems = new int[8]
        {
            wood, stone, iron, adamantium, mithrill, fabric, wool, leather
        };
        TargetItem = item;
        return this;
    }

    public CraftItem SetItemDB(ItemData item, int water, int apple)
    {
        NeedItems = new int[2]
        {
            water, apple
        };
        TargetItem = item;
        return this;
    }
}

public class CraftItemDB
{
    public List<CraftItem> CraftItems = new List<CraftItem>();
    
    public void Setting()
    {
        string line;
        string[] texts;
        int[] counts;

        using (StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"\Assets\CraftDB.txt"))
        {
            while((line = file.ReadLine()) != null)
            {
                texts = line.Split('\t');
                counts = new int[texts.Length];
                ImportItemDB(texts, counts);
            }
        }
    }

    //요구 재료를 반환하는 함수
    public ItemData[] GetItemStuff(ItemData needItem)
    {
        ItemData[] stuffs = null;

        foreach (CraftItem item in CraftItems)
        {
            if (item.TargetItem.Equals(needItem))
            {
                if (item.TargetItem.ItemType == ItemTypes.Eat)
                {
                    stuffs = new ItemData[item.NeedItems.Length];
                    for (int i = 0; i < item.NeedItems.Length; i++)
                    {
                        stuffs[i] = ItemList.Instance.ItemIndex[i + 60];
                        Debug.Log(ItemList.Instance.ItemIndex[i + 60].ItemType);
                    }
                    return stuffs;
                }
                else
                {
                    stuffs = new ItemData[item.NeedItems.Length];
                    for (int i = 0; i < item.NeedItems.Length; i++)
                    {
                        stuffs[i] = ItemList.Instance.ItemIndex[i + 51];
                        Debug.Log(ItemList.Instance.ItemIndex[i + 51].ItemType);
                    }
                    return stuffs;
                }
            }
        }
        return stuffs;
    }

    void ImportItemDB(string[] text, int[] count)
    {
        ItemData itemData;
        ItemCodes type = (ItemCodes)Enum.Parse(typeof(ItemCodes), text[0]);

        for (int i = 1; i < text.Length; i++)
        {
            count[i - 1] = int.Parse(text[i]);
        }

        for (int i = 0; i < ItemList.Instance.ItemIndex.Length; i++)
        {
            if(ItemList.Instance.ItemIndex[i].ItemCode != ItemCodes.Empty)
            {
                if (ItemList.Instance.ItemIndex[i].ItemCode == type)
                {
                    itemData = ItemList.Instance.ItemIndex[i];

                    //제작 재료 순서대로
                    //장비 : 나무, 돌, 철, 아다만티움, 미스릴, 천, 양모, 가죽
                    //물약 : 물, 사과
                    if (itemData.ItemType == ItemTypes.Eat)
                    {
                        CraftItems.Add(new CraftItem().SetItemDB(itemData, count[0], count[1]));
                    }
                    else
                    {
                        CraftItems.Add(new CraftItem().SetItemDB(itemData, count[0], count[1], count[2], count[3], count[4], count[5], count[6], count[7]));
                    }
                }
            }
            
        }
    }
}