using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMTest : MonoBehaviour {

    public enum MyEnum
    {
        Music1,
        Music2,
    }
    public MyEnum TestEnum;

    public Slider BGMSlider;
    private BGMManager BGM;
 
	// Use this for initialization
	void Start () {
        BGM = FindObjectOfType<BGMManager>();
    }
    public void OnClickedButton()
    {
        BGM.Play((int)TestEnum);
        BGM.FadeInMusic();
    }

    public void SetBGMVolume()
    {
        BGM.SetVolumn(BGMSlider.value);
    }

    /// <summary>
    /// BGM 볼륨 정보 함수
    /// </summary>
    /// <returns></returns>
    public float GetBGMVolume()
    {
        return BGMSlider.value;
    }
}
