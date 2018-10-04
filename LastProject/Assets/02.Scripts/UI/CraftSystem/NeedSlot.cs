using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedSlot : CraftSlotParent
{
    UILabel label;
    UISprite sprite;
    int currentCount = 0;
    int needCount = 0;
    public bool isCraft = false; 

    void Start ()
    {
        sprite = GetComponent<UISprite>();
        label = GetComponentInChildren<UILabel>();
        ResetCount();
    }

    public void ResetCount()
    {
        currentCount = 0;
        needCount = 0;
        SetTextAndColor();
        label.text = "- / -";
        ResetSprite();
    }

    public void SetCurrentCount(int count)
    {
        if(needCount <= 0)
        {
            Debug.Log("요구 수량이 0개 입니다. 요구 수량을 먼저 설정해주세요.");
            return;
        }

        currentCount = count;
        SetTextAndColor();
    }

    public void SetNeedCount(int count)
    {
        needCount = count;
        SetTextAndColor();
    }

    void SetTextAndColor()
    {
        label.text = currentCount + " / " + needCount;

        if (currentCount != needCount)
        {
            label.effectColor = Color.red;
            isCraft = false;
        }
        else
        {
            label.effectColor = Color.black;
            isCraft = true;
        }
    }

    public bool CountIsZero()
    {
        if (needCount == 0)
            return true;
        return false;
    }

    void ResetSprite()
    {
        ChangeSprite((CraftItemResource)999);
    }

    public void ChangeSprite(CraftItemResource index)
    {
        switch (index)
        {
            case CraftItemResource.Wood:
                sprite.spriteName = "NeedWood";
                break;
            case CraftItemResource.Stone:
                sprite.spriteName = "NeedStone";
                break;
            case CraftItemResource.Iron:
                sprite.spriteName = "NeedIron";
                break;
            case CraftItemResource.Adamantium:
                sprite.spriteName = "NeedAdamantium";
                break;
            case CraftItemResource.Mithrill:
                sprite.spriteName = "NeedMithrill";
                break;
            case CraftItemResource.Fabric:
                sprite.spriteName = "NeedFabric";
                break;
            case CraftItemResource.Wool:
                sprite.spriteName = "NeedWool";
                break;
            case CraftItemResource.Leather:
                sprite.spriteName = "NeedLeather";
                break;
            case CraftItemResource.Water:
                sprite.spriteName = "NeedWater";
                break;
            case CraftItemResource.Apple:
                sprite.spriteName = "NeedApple";
                break;
            case CraftItemResource.Empty:
            default:
                sprite.spriteName = "NeedEmpty";
                break;
        }
    }
}
