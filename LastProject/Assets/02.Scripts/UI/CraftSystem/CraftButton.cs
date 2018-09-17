using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    [SerializeField] CraftSystem craftSystem;

    void OnClick()
    {
        if (craftSystem.IsCrafty())
        {
            Debug.Log(craftSystem.SelectedItem.ItemName + "의 제작에 성공했습니다!");
            //craftSystem.InventorySystem.AddIteminInventory(craftSystem.SelectedItem);
        }
        else
            Debug.Log("응 실패");
    }
}
