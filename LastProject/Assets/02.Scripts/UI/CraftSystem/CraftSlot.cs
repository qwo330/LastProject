using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSlot : CraftSlotParent
{
    const string TAG_IMAGE = "CraftItemImage";
    const string TAG_DESCRIPTION = "CraftItemDescription";

    UISprite image;
    UISprite description;

    public ItemData itemData;

    private void Start()
    {
        UISprite[] sprite = GetComponentsInChildren<UISprite>();
        for (int i = 0; i < sprite.Length; i++)
        {
            if(sprite[i].tag == TAG_IMAGE)
            {
                image = sprite[i];
            }
            else if(sprite[i].tag == TAG_DESCRIPTION)
            {
                description = sprite[i];
            }
        }
        ChangeSprite();
    }

    void OnClick()
    {
        craftSystem.ViewNeedItems(itemData);
    }

    //private void OnEnable()
    //{
    //    ChangeSprite();
    //}

    public void ChangeSprite()
    {
        image.spriteName = itemData.ItemName;
    }
}
