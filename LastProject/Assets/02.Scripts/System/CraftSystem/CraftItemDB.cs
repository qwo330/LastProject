using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

    //제작 재료 순서대로
    //장비 : 나무, 돌, 철, 아다만티움, 미스릴, 천, 양모, 가죽
    //물약 : 물, 사과
    void ImportItemDB(string[] text, int[] count)
    {
        ItemData itemData;

        switch (text[0])
        {
            case "WoodSword":
                itemData = ItemLists.WoodSword;
                break;

            case "StoneSword":
                itemData = ItemLists.StoneSword;
                break;

            case "IronSword":
                itemData = ItemLists.IronSword;
                break;

            case "RedPotion":
                itemData = ItemLists.RedPotion;
                break;

            default:
                Debug.Log("DB 이름 불일치! WoodSword로 입력됩니다.");
                itemData = ItemLists.WoodSword;
                break;
        }

        for (int i = 1; i < text.Length; i++)
        {
            count[i - 1] = int.Parse(text[i]);
        }

        if(itemData.ItemType == ItemTypes.Eat)
        {
            CraftItems.Add(new CraftItem().SetItemDB(itemData, count[0], count[1]));
        }
        else
        {
            CraftItems.Add(new CraftItem().SetItemDB(itemData, count[0], count[1], count[2], count[3], count[4], count[5], count[6], count[7]));
        }

        Debug.Log("아이템 생성 성공! : " + itemData.ItemName);
    }
}