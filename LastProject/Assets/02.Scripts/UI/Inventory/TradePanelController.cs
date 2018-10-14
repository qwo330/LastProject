using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradePanelController : MonoBehaviour {
    public GameObject TradePanel;
    public GameObject TradeSlotPrefab;

    public int SoldItemCount;

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
        int count = 0;
        for (int i = 0; i < SoldItemCount; i++)
        {
            GameObject obj = Instantiate(TradeSlotPrefab, TradePanel.transform);
            RectTransform rt = TradePanel.GetComponent<RectTransform>();
            if (i > 4)
                rt.offsetMin = new Vector2(0, rt.offsetMin.y - 104f);
        }
    }
}
