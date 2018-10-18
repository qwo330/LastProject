using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIOnAndOff : MonoBehaviour {

    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject quest;
    [SerializeField]
    private GameObject craft;
    [SerializeField]
    private GameObject monsterBook;

    public void TurnOnInventory()
    {
        

        InventorySystem a;
        a = StageManager.Instance.UICanvas.GetComponentInChildren<InventorySystem>();

        if(inventory == null)
            inventory = StageManager.Instance.UICanvas.GetComponentInChildren<InventorySystem>().gameObject;
        inventory.SetActive(true);
    }
    public void TurnOffInventory()
    {
        inventory.SetActive(false);
    }

    public void TurnOnQuest()
    {
        quest.transform.position = new Vector3(0,0,0);
    }
    public void TurnOffQuest()
    {
        quest.transform.position = new Vector3(0, -2, 0);
    }

    public void TurnOnMonsterBook()
    {
        monsterBook.SetActive(true);
    }
    public void TurnOffMonsterBook()
    {
        monsterBook.SetActive(false);
    }

    public void TurnOnCraft()
    {
        craft.SetActive(true);
    }
   
}
