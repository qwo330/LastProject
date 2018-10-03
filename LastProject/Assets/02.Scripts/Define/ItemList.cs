using UnityEngine;

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

    Count,
}

public class ItemList : Singleton<ItemList>
{
    public ItemData[] ItemIndex;
    public int EquipmentCount {private set; get;}
    public int FoodCount { private set; get; }
    public int RawMaterialCount { private set; get; }

    public void Init()
    {
        setItemData();
        getItemTypeCount();
    }

    void setItemData()
    {
        ItemIndex = new ItemData[60];

        ItemIndex[0] = new ItemData(0, 0, 0, 0);
        ItemIndex[1] = new ItemData(ItemCodes.WoodSword, ItemTypes.Weapon, 5, 5);
        ItemIndex[2] = new ItemData(ItemCodes.StoneSword, ItemTypes.Weapon, 7, 7);
        ItemIndex[3] = new ItemData(ItemCodes.IronSword, ItemTypes.Weapon, 10, 10);

        ItemIndex[11] = new ItemData(ItemCodes.FabricHelmet, ItemTypes.Helmet, 5, 5);
        ItemIndex[12] = new ItemData(ItemCodes.LeatherHelmet, ItemTypes.Helmet, 7, 7);
        ItemIndex[13] = new ItemData(ItemCodes.IronHelmet, ItemTypes.Helmet, 10, 10);

        ItemIndex[21] = new ItemData(ItemCodes.FabricArmor, ItemTypes.Armor, 5, 5);
        ItemIndex[22] = new ItemData(ItemCodes.LeatherArmor, ItemTypes.Armor, 7, 7);
        ItemIndex[23] = new ItemData(ItemCodes.IronArmor, ItemTypes.Armor, 10, 10);

        ItemIndex[31] = new ItemData(ItemCodes.FabricShoes, ItemTypes.Shoes, 5, 5);
        ItemIndex[32] = new ItemData(ItemCodes.LeatherShoes, ItemTypes.Shoes, 7, 7);
        ItemIndex[33] = new ItemData(ItemCodes.IronShoes, ItemTypes.Shoes, 10, 10);

        ItemIndex[41] = new ItemData(ItemCodes.HalfRedPotion, ItemTypes.Eat, 10, 25);
        ItemIndex[42] = new ItemData(ItemCodes.RedPotion, ItemTypes.Eat, 10, 50);
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