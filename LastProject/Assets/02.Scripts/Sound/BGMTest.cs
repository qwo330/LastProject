using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMTest : MonoBehaviour {

   
	void Start () {
       
    }
    public void OnClickedButton()
    {
        BGMManager.Instance.Play(BGMLIst.BGM2);
        BGMManager.Instance.FadeInMusic();
    }

    public void SetBGMVolume()
    {
        BGMManager.Instance.SetVolumn();
    }
 
}
