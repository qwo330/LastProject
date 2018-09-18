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
}

public static class ItemLists
{
    public static ItemData WoodSword = new ItemData(ItemCodes.WoodSword, ItemTypes.Weapon, 5, 5);
    public static ItemData StoneSword = new ItemData(ItemCodes.StoneSword, ItemTypes.Weapon, 7, 7);
    public static ItemData IronSword = new ItemData(ItemCodes.IronSword, ItemTypes.Weapon, 10, 10);

    public static ItemData FabricHelmet = new ItemData(ItemCodes.FabricHelmet, ItemTypes.Helmet, 5, 5);
    public static ItemData LeatherHelmet = new ItemData(ItemCodes.LeatherHelmet, ItemTypes.Helmet, 7, 7);
    public static ItemData IronHelmet = new ItemData(ItemCodes.IronHelmet, ItemTypes.Helmet, 10, 10);

    public static ItemData FabricArmor = new ItemData(ItemCodes.FabricArmor, ItemTypes.Armor, 5, 5);
    public static ItemData LeatherArmor = new ItemData(ItemCodes.LeatherArmor, ItemTypes.Armor, 7, 7);
    public static ItemData IronArmor = new ItemData(ItemCodes.IronArmor, ItemTypes.Armor, 10, 10);

    public static ItemData FabricShoes = new ItemData(ItemCodes.FabricShoes, ItemTypes.Shoes, 5, 5);
    public static ItemData LeatherShoes = new ItemData(ItemCodes.LeatherShoes, ItemTypes.Shoes, 7, 7);
    public static ItemData IronShoes = new ItemData(ItemCodes.IronShoes, ItemTypes.Shoes, 10, 10);

    public static ItemData HalfRedPotion = new ItemData(ItemCodes.HalfRedPotion, ItemTypes.Eat, 10, 25);
    public static ItemData RedPotion = new ItemData(ItemCodes.RedPotion, ItemTypes.Eat, 10, 50);
}