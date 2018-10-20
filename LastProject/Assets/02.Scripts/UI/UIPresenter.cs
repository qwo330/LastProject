using UnityEngine;

public class UIPresenter : Singleton<UIPresenter> {

    public GameObject UICanvas;
    public GameObject UIRoot;

    public GameObject Inventory;
    public PlayerStatusUI PlayerStatusUI;

    public GameObject quest;
    public GameObject craft;
    public GameObject monsterBook;

	public void Init()
    {
        UICanvas = Instantiate(Resources.Load("Prefabs/UI Canvas"), transform) as GameObject;
        UIRoot = Instantiate(Resources.Load("Prefabs/UI Root"), transform) as GameObject;

        Inventory = UICanvas.GetComponentInChildren<InventorySystem>().gameObject;
        PlayerStatusUI = UIRoot.GetComponentInChildren<PlayerStatusUI>();
        craft = UIRoot.GetComponentInChildren<CraftSystem>().gameObject;
        quest = UIRoot.GetComponentInChildren<QuestUI>().gameObject;
        //monsterBook = UIRoot.GetComponentInChildren<>

        Inventory.SetActive(false);
        quest.transform.position = new Vector3(0, -2, 0);
        craft.SetActive(false);
        //monsterBook.SetActive(false);

        UICanvas.SetActive(false);
        UIRoot.SetActive(false);
    }
}
