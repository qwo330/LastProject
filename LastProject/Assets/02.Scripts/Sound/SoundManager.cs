using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = Clip;
    }

    public void Play()
    {
        source.volume = Volume;
        source.pitch = Pitch;
        source.Play();
    }
}



public class SoundManager : Singleton<SoundManager>
{
    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("한개이상의 사운드 매니저가 존재합니다.");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("Sound_"+i+"_"+sounds[i].Name);
            soundObject.transform.SetParent(this.transform);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].Name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("SoundManager : 사운드 리스트를 찾을 수 없습니다." + _name);
    }

    //슬라이더에 연동시켜줄 예정
    public float MusicVolume;
    public void ChangeVolumeValue()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Volume = MusicVolume;
        }
    }
}
