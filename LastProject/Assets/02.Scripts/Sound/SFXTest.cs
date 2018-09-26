using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXTest : MonoBehaviour {

    
    public enum MyEnum
    {
        Music3,
    }
    public MyEnum SFXEnum;
    
    public Slider SFXslider;

  
    public string Test3;

    private AudioManager sfx;

    // Use this for initialization
    void Start () {
        sfx = FindObjectOfType<AudioManager>();
	}

  
    public void OnClickedButtone()
    {
        sfx.Play(SFXEnum.ToString());
    }

   
    public void SetSFXVolume()
    {
        sfx.SetSFXVolume(SFXslider.value);
    }


    /// <summary>
    ///SFX 볼륨 정보 함수
    /// </summary>
    /// <returns></returns>
   
    public float GetSFXVolume()
    {
        return SFXslider.value;
    }

}
