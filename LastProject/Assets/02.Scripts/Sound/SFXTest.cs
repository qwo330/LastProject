using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXTest : MonoBehaviour {

   
    public void OnClickedButtone()
    {
        AudioManager.Instance.Play(SoundList.SFX2);
    }

    public void OnDragSlider()
    {
        AudioManager.Instance.SetSFXVolume();
    }
   
}
