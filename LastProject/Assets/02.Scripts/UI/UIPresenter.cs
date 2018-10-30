using UnityEngine;

public class UIPresenter : Singleton<UIPresenter> {

    public GameObject UICanvas;
    public GameObject UIRoot;

    public InventorySystem Inventory;
    public GameObject InventoryObj;
    public PlayerStatusUI PlayerStatusUI;

    public GameObject quest;
    public CraftSystem craft;
    public GameObject craftObj;
    public GameObject monsterBook;

	public void Init()
    {
        UICanvas = Instantiate(Resources.Load("Prefabs/UI Canvas"), transform) as GameObject;
        UIRoot = Instantiate(Resources.Load("Prefabs/UI Root"), transform) as GameObject;

        Inventory = UICanvas.GetComponentInChildren<InventorySystem>();
        InventoryObj = Inventory.gameObject;
        PlayerStatusUI = UIRoot.GetComponentInChildren<PlayerStatusUI>();
        craft = UIRoot.GetComponentInChildren<CraftSystem>();
        craftObj = craft.gameObject;
        quest = UIRoot.GetComponentInChildren<QuestUI>().gameObject;

        //monsterBook = UIRoot.GetComponentInChildren<>

        Inventory.gameObject.SetActive(false);
        quest.transform.position = new Vector3(0, -2, 0);
        craft.InventorySystem = Inventory;
        craft.Init();
        craft.gameObject.SetActive(false);
        //monsterBook.SetActive(false);

        UICanvas.SetActive(false);
        UIRoot.SetActive(false);
    }
}
