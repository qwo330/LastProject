//using UnityEngine;

public enum ItemTypes
{
    Eat = 1,
    RawMaterial,

    Weapon,
    Helmet,
    Armor,
    Shoes,
}

public enum ItemCodes
{
    Empty = 0,

    WoodSword = 1,
    StoneSword = 2,
    IronSword = 3,
    AdamantiumSword = 4,
    MithrilSword = 5,

    FabricHelmet = 11,
    LeatherHelmet,
    IronHelmet,
    AdamantiumHelmet,
    MithrilHelmet,

    FabricArmor = 21,
    LeatherArmor,
    IronArmor,
    AdamantiumArmor,
    MithrilArmor,

    FabricShoes = 31,
    LeatherShoes,
    IronShoes,
    AdamantiumShoes,
    MithrilShoes,

    HalfRedPotion = 41,
    RedPotion = 42,

    //제작 재료 순서대로 반드시 지켜야 함
    //장비 : 나무, 돌, 철, 아다만티움, 미스릴, 천, 양모, 가죽
    //물약 : 물, 사과
    Wood = 51,
    Stone,
    Iron,
    Adamantium,
    Mithrill,
    Fabric,
    Wool,
    Leather,

    Water = 60,
    Apple,

    Count,
}

public class ItemList : Singleton<ItemList>
{
    public ItemData[] ItemIndex;
    public int EquipmentCount { get; private set; }
    public int FoodCount { get; private set; }
    public int RawMaterialCount { get; private set; }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        setItemData();
        getItemTypeCount();
    }

    void setItemData()
    {
        ItemIndex = new ItemData[99];

        ItemIndex[0] = new ItemData(0, 0, 0, 0, 0);
        ItemIndex[1] = new ItemData(ItemCodes.WoodSword, ItemTypes.Weapon, 5, 5, 500);
        ItemIndex[2] = new ItemData(ItemCodes.StoneSword, ItemTypes.Weapon, 7, 7, 1000);
        ItemIndex[3] = new ItemData(ItemCodes.IronSword, ItemTypes.Weapon, 10, 10, 3000);

        ItemIndex[11] = new ItemData(ItemCodes.FabricHelmet, ItemTypes.Helmet, 5, 5, 500);
        ItemIndex[12] = new ItemData(ItemCodes.LeatherHelmet, ItemTypes.Helmet, 7, 7, 1000);
        ItemIndex[13] = new ItemData(ItemCodes.IronHelmet, ItemTypes.Helmet, 10, 10, 3000);

        ItemIndex[21] = new ItemData(ItemCodes.FabricArmor, ItemTypes.Armor, 5, 5, 500);
        ItemIndex[22] = new ItemData(ItemCodes.LeatherArmor, ItemTypes.Armor, 7, 7, 1000);
        ItemIndex[23] = new ItemData(ItemCodes.IronArmor, ItemTypes.Armor, 10, 10, 3000);

        ItemIndex[31] = new ItemData(ItemCodes.FabricShoes, ItemTypes.Shoes, 5, 5, 500);
        ItemIndex[32] = new ItemData(ItemCodes.LeatherShoes, ItemTypes.Shoes, 7, 7, 1000);
        ItemIndex[33] = new ItemData(ItemCodes.IronShoes, ItemTypes.Shoes, 10, 10, 3000);

        ItemIndex[41] = new ItemData(ItemCodes.HalfRedPotion, ItemTypes.Eat, 10, 25, 1000);
        ItemIndex[42] = new ItemData(ItemCodes.RedPotion, ItemTypes.Eat, 10, 50, 4000);

        //ItemIndex[51] = new ItemData(ItemCodes.Herb, ItemTypes.RawMaterial, 999, 0, 800);
        //ItemIndex[53] = new ItemData(ItemCodes.Meat, ItemTypes.RawMaterial, 999, 0, 1000);
        //ItemIndex[55] = new ItemData(ItemCodes.Mushroom, ItemTypes.RawMaterial, 999, 0, 800);
        //ItemIndex[56] = new ItemData(ItemCodes.Salt, ItemTypes.RawMaterial, 999, 0, 200);
        //ItemIndex[62] = new ItemData(ItemCodes.Ruby, ItemTypes.RawMaterial, 999, 0, 9000);
        //ItemIndex[63] = new ItemData(ItemCodes.Cotton, ItemTypes.RawMaterial, 999, 0, 800);
        ItemIndex[51] = new ItemData(ItemCodes.Wood, ItemTypes.RawMaterial, 999, 0, 700);
        ItemIndex[52] = new ItemData(ItemCodes.Stone, ItemTypes.RawMaterial, 999, 0, 300);
        ItemIndex[53] = new ItemData(ItemCodes.Iron, ItemTypes.RawMaterial, 999, 0, 1300);
        ItemIndex[54] = new ItemData(ItemCodes.Adamantium, ItemTypes.RawMaterial, 999, 0, 5000);
        ItemIndex[55] = new ItemData(ItemCodes.Mithrill, ItemTypes.RawMaterial, 999, 0, 12000);
        ItemIndex[56] = new ItemData(ItemCodes.Fabric, ItemTypes.RawMaterial, 999, 0, 1500);
        ItemIndex[57] = new ItemData(ItemCodes.Wool, ItemTypes.RawMaterial, 999, 0, 1000);
        ItemIndex[58] = new ItemData(ItemCodes.Leather, ItemTypes.RawMaterial, 999, 0, 2000);

        ItemIndex[60] = new ItemData(ItemCodes.Water, ItemTypes.RawMaterial, 999, 0, 800);
        ItemIndex[61] = new ItemData(ItemCodes.Apple, ItemTypes.RawMaterial, 999, 0, 500);
    }

    void getItemTypeCount()
    {
        EquipmentCount = 0; FoodCount = 0; RawMaterialCount = 0;

        for (int i = 0; i < ItemIndex.Length; i++)
        {
            if (ItemIndex[i].ItemType == ItemTypes.Eat)
                FoodCount++;
            else if (ItemIndex[i].ItemType == ItemTypes.RawMaterial)
                RawMaterialCount++;
            else if (ItemIndex[i].ItemType == ItemTypes.Weapon || ItemIndex[i].ItemType == ItemTypes.Armor
                || ItemIndex[i].ItemType == ItemTypes.Helmet || ItemIndex[i].ItemType == ItemTypes.Shoes)
            {
                EquipmentCount++;
            }
        }
    }
}