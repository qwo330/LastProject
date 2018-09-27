using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTest : MonoBehaviour {

   
    public void OnClickedSFXButtone()
    {
        AudioManager.Instance.SFXPlay(SoundList.SFX1);
    }

    public void OnDragSFXSlider()
    {
        AudioManager.Instance.SetSFXVolume();
    }

    public void OnClickedBGMButton()
    {
        AudioManager.Instance.BGMPlay(BGMLIst.BGM2);
        AudioManager.Instance.FadeInBGMMusic();
    }

    public void OnDragBGMSlider()
    {
        AudioManager.Instance.SetBGMVolumn();
    }

}
