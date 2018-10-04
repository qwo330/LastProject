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
                Debug.Log(craftSystem.SelectedItem.ItemName + "의 제작에 성공했습니다!");
                craftSystem.InventorySystem.AddIteminInventory(craftSystem.SelectedItem);
            }
        }
        else
            Debug.Log("응 실패");
    }
}
