using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherRawMaterial : MonoBehaviour {
    [SerializeField]
    ItemCodes itemCode;
    ItemList itemList;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            InventorySystem a;
            //if (inventory.FindEmptySlot != -1)
            {
                Debug.Log("아이템 추가");
                // TODO: inventory.AddIteminInventory(itemList.ItemIndex[(int)itemCode]);
                // TODO : ObjectPool.push(this.gameobject);
            }
        }
    }
}
