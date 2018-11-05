using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIOnAndOff : MonoBehaviour {

    public void TurnOnInventory()
    {
        UIPresenter.Instance.Inventory.gameObject.SetActive(true);
    }
    public void TurnOffInventory()
    {
        UIPresenter.Instance.Inventory.gameObject.SetActive(false);
    }

    public void TurnOnQuest()
    {
        UIPresenter.Instance.quest.transform.position = new Vector3(0,0,0);
    }
    public void TurnOffQuest()
    {
        UIPresenter.Instance.quest.transform.position = new Vector3(0, -2, 0);
    }

    //public void TurnOnMonsterBook()
    //{
    //    UIPresenter.Instance.monsterBook.SetActive(true);
    //}
    //public void TurnOffMonsterBook()
    //{
    //    UIPresenter.Instance.monsterBook.SetActive(false);
    //}

    public void TurnOnCraft()
    {
        UIPresenter.Instance.craft.gameObject.SetActive(true);
    }
   
}
