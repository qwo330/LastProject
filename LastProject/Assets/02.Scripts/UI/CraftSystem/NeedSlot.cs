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
            if(needCount == 0)
            {
                isCraft = false;
            }
            else
            {
                isCraft = true;
            }
        }
    }

    void ResetSprite()
    {

    }

    public override void ChangeSprite(ItemData itemData)
    {
        
    }
}
