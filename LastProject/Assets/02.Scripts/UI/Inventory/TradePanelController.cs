using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradePanelController : MonoBehaviour
{
    public GameObject EquipmentPanel, FoodPanel;
    public GameObject TradeListPrefab;
    public ScrollRect Mask;

    public GraphicRaycaster gr;
    PointerEventData ped;

    public GameObject TradePopUp;
    public Image TradePopUpImg;
    Text tradePopUpText;
    InventorySystem inventory;

    public int EquipmentCount, FoodCount;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        ped = new PointerEventData(null);

        CreateTradePanel();
        inventory = GetComponentInParent<InventorySystem>();

        // Button의 Text가 잡히지 않게 주의할것.
        tradePopUpText = TradePopUp.GetComponentInChildren<Text>();
        TradePopUp.SetActive(false);
    }

    void CreateTradePanel()
    {
        for (int i = 0; i < EquipmentCount; i++)
        {
            GameObject obj = Instantiate(TradeListPrefab, EquipmentPanel.transform);
            SaleItemSlot slot = obj.GetComponent<SaleItemSlot>();
            //NPC가 판매할 아이템 정보
            //slot.SalesItem = 

            Button btn = obj.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => Buy(obj));

            Text text = obj.GetComponentInChildren<Text>();
            text.text = "ItemName\n" + "ItemCost\n" + "ItemDetailText";

            RectTransform rt = EquipmentPanel.GetComponent<RectTransform>();
            if (i > 4)
                rt.offsetMin = new Vector2(0, rt.offsetMin.y - 104f);
        }

        for (int i = 0; i < FoodCount; i++)
        {
            GameObject obj = Instantiate(TradeListPrefab, FoodPanel.transform);
            SaleItemSlot slot = obj.GetComponent<SaleItemSlot>();
            //NPC가 판매할 아이템 정보
            //slot.SalesItem = 

            Button btn = obj.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => Buy(obj));

            Text text = obj.GetComponentInChildren<Text>();
            text.text = "ItemName\n" + "ItemCost\n" + "ItemDetailText";

            RectTransform rt = FoodPanel.GetComponent<RectTransform>();
            if (i > 4)
                rt.offsetMin = new Vector2(0, rt.offsetMin.y - 104f);
        }
    }

    public void Buy(GameObject obj)
    {
        SaleItemSlot target = obj.GetComponent<SaleItemSlot>();
        //ItemData item = target.SalesItem;
        //TradePopUp.SetActive(true);
        //tradePopUpText.text = item.ItemName + "\n" + item.Cost + "\n";
    }

    public void ChangeFoodTap()
    {
        FoodPanel.SetActive(true);
        EquipmentPanel.SetActive(false);
        Mask.content = FoodPanel.GetComponent<RectTransform>();
    }

    public void ChangeEquipmentTap()
    {
        EquipmentPanel.SetActive(true);
        FoodPanel.SetActive(false);
        Mask.content = EquipmentPanel.GetComponent<RectTransform>();
    }
}
