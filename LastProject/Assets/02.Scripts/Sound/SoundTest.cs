using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTest : MonoBehaviour {

    
    public enum MyEnum
    {
        Music3,
    }
    public MyEnum SFXEnum;
    
    public Slider SFXslider;

  
    public string Test3;

    private AudioManager audio;

    // Use this for initialization
    void Start () {
        audio = FindObjectOfType<AudioManager>();
	}

  
    public void OnClickedButtone3()
    {
        audio.Play(SFXEnum.ToString());
    }

   
    public void SetSFXVolume()
    {
        audio.SetSFXVolume(SFXslider.value);
    }


    /// <summary>
    ///SFX볼륨 정보 함수
    /// </summary>
    /// <returns></returns>
   
    public float GetSFXVolume()
    {
        return SFXslider.value;
    }

}
