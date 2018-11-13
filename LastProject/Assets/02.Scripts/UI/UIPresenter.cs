using UnityEngine;

public class UIPresenter : Singleton<UIPresenter> {

    public GameObject UICanvas;
    public GameObject UIRoot;

    public InventorySystem Inventory;
    public PlayerStatusUI PlayerStatusUI;

    public GameObject quest;
    public CraftSystem craft;

    public ImageStorage ImageStorage;


    public void Init()
    {
        UICanvas = Instantiate(Resources.Load("Prefabs/UI Canvas"), transform) as GameObject;
        UIRoot = Instantiate(Resources.Load("Prefabs/UI Root"), transform) as GameObject;

        Inventory = UICanvas.GetComponentInChildren<InventorySystem>();
        PlayerStatusUI = UIRoot.GetComponentInChildren<PlayerStatusUI>();
        craft = UIRoot.GetComponentInChildren<CraftSystem>();
        quest = UIRoot.GetComponentInChildren<QuestUI>().gameObject;

        Inventory.Init();
        Inventory.gameObject.SetActive(false);

        quest.transform.position = new Vector3(0, -2, 0);

        craft.InventorySystem = Inventory;
        craft.Init();
        craft.gameObject.SetActive(false);

        ImageStorage = GameObject.FindGameObjectWithTag("ImageStorage").GetComponent<ImageStorage>();
        ImageStorage.Init();

        UICanvas.SetActive(false);
        UIRoot.SetActive(false);
    }

    public void DrawPlayerUI(CharacterStatus status)
    {
        if (PlayerStatusUI != null)
        {
            PlayerStatusUI.ChangeStatus(status);
        }
    }
}
