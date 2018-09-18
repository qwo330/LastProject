using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    [Range(0,1)]
    public float TestValue;

    public UISprite bar;

    private void Start()
    {
        if (this.bar == null)
        {
            bar = this.GetComponent<UISprite>();
            bar.fillAmount = TestValue;
        }
    }

    public void OnClick()
    {
        TestValue += 0.1f;
        bar.fillAmount = TestValue;
    }

}
