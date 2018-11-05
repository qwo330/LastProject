using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    [SerializeField] CraftSystem craftSystem;
    int slotEmptyCount;

    void OnClick()
    {
        if (craftSystem.IsCrafty(out slotEmptyCount))
        { 
            if (slotEmptyCount == 0)
            {
                Debug.Log("제작할 아이템이 없습니다!");
            }
            else
            {
                Debug.Log(craftSystem.SelectedItem.TargetItem.ItemName + "의 제작에 성공했습니다!");
                if(craftSystem.StuffItems != null)
                {
                    for (int i = 0; i < craftSystem.StuffItems.Length; i++)
                    {
                        craftSystem.InventorySystem.RemoveIteminInventory(craftSystem.StuffItems[i], craftSystem.SelectedItem.NeedItems[i]);
                    }
                    craftSystem.InventorySystem.AddIteminInventory(craftSystem.SelectedItem.TargetItem);
                }
                else
                {
                    Debug.Log("제작 재료 리스트를 받지 못함");
                }
                
            }
        }
        else
            Debug.Log("응 실패");
    }
}
