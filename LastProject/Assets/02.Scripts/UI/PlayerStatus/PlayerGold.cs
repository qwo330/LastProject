using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour {

    public UILabel GoldObj;
    public int TestGold;

    private void Start()
    {
        this.GoldObj = GetComponent<UILabel>();
        GoldObj.text = TestGold.ToString();
    }

    public void OnClick()
    {
        TestGold += 100;
        GoldObj.text = TestGold.ToString();
    }
}
