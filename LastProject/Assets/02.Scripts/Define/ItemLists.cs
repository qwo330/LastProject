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

    Cloth = 11,
    LeatherArmor = 12,
    IronArmor = 13,
    AdamantiumArmor =14,
    MithrilArmor = 15,

    RedPotion = 21,
}

public static class ItemLists
{
    public static ItemData WoodSword = new ItemData(ItemCodes.WoodSword, ItemTypes.Weapon, 5, 5);
    public static ItemData StoneSword = new ItemData(ItemCodes.StoneSword, ItemTypes.Weapon, 7, 7);
    public static ItemData IronSword = new ItemData(ItemCodes.IronSword, ItemTypes.Weapon, 10, 10);

    public static ItemData RedPotion = new ItemData(ItemCodes.RedPotion, ItemTypes.Eat, 25, 25);

}