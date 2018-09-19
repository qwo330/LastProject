public enum ItemTypes
{
    Eat,
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

    private void Start()
    {
        SetItemData();
    }

    void SetItemData()
    {
        ItemIndex = new ItemData[60];

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
}