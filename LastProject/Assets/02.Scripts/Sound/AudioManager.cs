using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
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

public class AudioManager : MonoBehaviour
{

    static public AudioManager instance;

    [SerializeField]
    public Sound[] sounds;

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
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + "=" + sounds[i].Name);
            sounds[i].Setsource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void Play(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].Name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].Name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].Name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].Name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }
 
    public void SetSFXVolume( float voluem)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Volume = voluem;
            sounds[i].SetVolume();
        }
    }
    
}
