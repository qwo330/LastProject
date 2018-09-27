using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundList
{
    SFX1,
    SFX2,
    SFX3,
}
public enum BGMLIst
{
    BGM1,
    BGM2,
    BGM3,
}
[System.Serializable]
public class Sound
{
    [HideInInspector]
    public SoundList Name; 
    public AudioClip Clip; //사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float Volume;
    public bool Loop;

    public void Setsource(AudioSource source)
    {
        this.source = source;
        source.clip = Clip;
        source.loop = Loop;
    }

    public void SetVolume()
    {
        source.volume = Volume;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }

}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    public Sound[] sounds; // SFX 사운드 이펙트들
    [SerializeField]
    public AudioClip[] clips; // BGM 배경음악들

    private AudioSource source;

    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider BGMSlider;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
  
    public void SetSFXVolume()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Volume = volumeSlider.value;
            sounds[i].SetVolume();
        }
    }

    //SFX볼륨데이터
    public float GetSFXVolume()
    {
        return volumeSlider.value;
    }

    public void SetBGMVolumn()
    {
        source.volume = BGMSlider.value;
    }
  
    //BGM볼륨 데이터
    public float GetBGMVolume()
    {
        return BGMSlider.value;
    }

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    void Start()
    {
        Initialized();
        source = GetComponent<AudioSource>();   //BGM은 하나만 존재
    }

    public void Initialized()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + "=" + sounds[i].Name);
            sounds[i].Setsource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    /// <summary>
    /// SFX
    /// </summary>
    /// <param name="name"></param>
    public void SFXPlay(SoundList name)
    {
        sounds[(int)name].Play();
    }

    public void SFXStop(SoundList name)
    {
        sounds[(int)name].Stop();
    }

    public void SetSFXLoop(SoundList name)
    {
        sounds[(int)name].SetLoop();
    }

    public void SetSFXLoopCancel(SoundList name)
    {
        sounds[(int)name].SetLoopCancel();
    }

    /// <summary>
    ///BGM
    /// </summary>
    /// <param name="list"></param>
    public void BGMPlay(BGMLIst list)
    {
        source.volume = 1f;
        source.clip = clips[(int)list];
        source.Play();
    }

    public void PauseBGM()
    {
        source.Pause();
    }

    public void UnpauseBGM()
    {
        source.UnPause();
    }

    public void StopBGM()
    {
        source.Stop();
    }

    public void FadeOutBGMMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void FadeInBGMMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
}
 