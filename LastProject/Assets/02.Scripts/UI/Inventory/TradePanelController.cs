using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradePanelController : MonoBehaviour {
    public GameObject EquipmentPanel, FoodPanel;
    public GameObject TradeListPrefab;
    public ScrollRect Mask;

    public int EquipmentCount, FoodCount;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        CreateTradePanel();
    }

    private void CreateTradePanel()
    {
        for (int i = 0; i < EquipmentCount; i++)
        {
            GameObject obj = Instantiate(TradeListPrefab, EquipmentPanel.transform);
            RectTransform rt = EquipmentPanel.GetComponent<RectTransform>();
            if (i > 4)
                rt.offsetMin = new Vector2(0, rt.offsetMin.y - 104f);
        }

        for (int i = 0; i < FoodCount; i++)
        {
            GameObject obj = Instantiate(TradeListPrefab, FoodPanel.transform);
            RectTransform rt = FoodPanel.GetComponent<RectTransform>();
            if (i > 4)
                rt.offsetMin = new Vector2(0, rt.offsetMin.y - 104f);
        }
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
