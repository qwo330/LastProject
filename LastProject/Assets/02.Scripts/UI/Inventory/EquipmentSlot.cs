using UnityEngine;

public enum EquipmentType { Weapon, Helmet, Armor, Shoes, }

public abstract class Slot : MonoBehaviour
{
    public ItemData Item;
}

public class EquipmentSlot : Slot {
    
    public ItemTypes Part;
}