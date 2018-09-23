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

    public int PlayMusicTrack;

	// Use this for initialization
	void Start () {
        BGM = FindObjectOfType<BGMManager>();
    }
    public void OnClickedButton()
    {
        BGM.Play(TestEnum.ToString());
        BGM.FadeInMusic();
    }

    public void SetBGMVolume()
    {
        BGM.SetBGMVolume(BGMSlider.value);
    }

    public float GetBGMVolume()
    {
        return BGMSlider.value;
    }
}
