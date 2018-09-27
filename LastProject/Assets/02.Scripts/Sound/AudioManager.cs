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
    public Sound[] sounds;

    [SerializeField]
    private Slider volumeSlider;

    //SFX볼륨데이터
    public void SetSFXVolume()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Volume = volumeSlider.value;
            sounds[i].SetVolume();
        }
    }

    public float GetSFXVolume()
    {
        return volumeSlider.value;
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

    public void Play(SoundList name)
    {
        sounds[(int)name].Play();
    }

    public void Stop(SoundList name)
    {
        sounds[(int)name].Stop();
    }

    public void SetLoop(SoundList name)
    {
        sounds[(int)name].SetLoop();
    }

    public void SetLoopCancel(SoundList name)
    {
        sounds[(int)name].SetLoopCancel();
    }

}
