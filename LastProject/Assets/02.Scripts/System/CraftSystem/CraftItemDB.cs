//무기 재료 : 나무, 돌, 철, 아다만티움, 미스릴, 
//방어구 재료 : 목화(천), 양털, 가죽, 철, 아다만티움, 미스릴
//식재료 : 물, 풀, 버섯, 사과, 고기, 소금
using System.Collections.Generic;

public struct CraftItem
{
    public ItemData TargetItem;
    public int NeedWood;
    public int NeedStone;
    public int NeedIron;
    public int NeedAdamantium;
    public int NeedMithril;
    public int NeedFabric;
    public int NeedWool;
    public int NeedLeather;
    public int NeedWater;
    public int NeedApple;

    public CraftItem SetItemDB(ItemData item, int wood, int stone, int iron,
        int adamantium, int mithrill, int fabric, int wool, int leather)
    {
        TargetItem = item;
        NeedWood = wood;
        NeedStone = stone;
        NeedIron = iron;
        NeedAdamantium = adamantium;
        NeedMithril = mithrill;
        NeedFabric = fabric;
        NeedWool = wool;
        NeedLeather = leather;

        return this;
    }

    public CraftItem SetItemDB(ItemData item, int water, int apple)
    {
        TargetItem = item;
        NeedWater = water;
        NeedApple = apple;

        return this;
    }
}

public class CraftItemDB
{
    //제작 재료 순서대로
    //나무, 돌, 철, 아다만티움, 미스릴, 천, 양모, 가죽
    public List<CraftItem> CraftItems = new List<CraftItem>();
    
    public void Setting()
    {
        CraftItems.Add(new CraftItem().SetItemDB(ItemLists.WoodSword, 4, 0, 0, 0, 0, 0, 0, 0));
        CraftItems.Add(new CraftItem().SetItemDB(ItemLists.StoneSword, 1, 3, 0, 0, 0, 0, 0, 0));
        CraftItems.Add(new CraftItem().SetItemDB(ItemLists.IronSword, 1, 0, 3, 0, 0, 0, 0, 0));

        CraftItems.Add(new CraftItem().SetItemDB(ItemLists.RedPotion, 2, 1));
    }
}
