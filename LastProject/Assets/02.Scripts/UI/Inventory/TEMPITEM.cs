using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable { }

public abstract class TEMPITEM
{
    public ItemCodes ItemCode { get; protected set; }
    public int Count { get; set; }

    public Sprite sprite;
}

public struct SWORDTEMP
{
    public ItemCodes ItemCode { get; private set; }
    public int Count { get; set; }
    public Sprite sprite;
}

public class TEMPSWORD : TEMPITEM
{
    private void Start()
    {
        sprite = Resources.Load("TEMPSWORD") as Sprite;
        ItemCode = ItemCodes.TEMPSWORD;
        Count = 1;
    }
}

public class TEMPPOTION : TEMPITEM, IStackable
{
    private void Start()
    {
        sprite = Resources.Load("TEMPPOSITON") as Sprite;
        ItemCode = ItemCodes.TEMPPOTION;
        Count = 1;
    }
}