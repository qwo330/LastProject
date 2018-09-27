using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BGMLIst
{
    BGM1,
    BGM2,
    BGM3,
}


public class BGMManager : Singleton<BGMManager>
{

    public AudioClip[] clips; // 배경음악들

    private AudioSource source;

    [SerializeField]
    private Slider BGMSlider;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    //BGM볼륨 데이터
    public float GetBGMVolume()
    {
        return BGMSlider.value;
    }

    static public BGMManager instance;

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
        source = GetComponent<AudioSource>();   //BGM은 하나만 존재
    }

    public void Play(BGMLIst list)
    {
        source.volume = 1f;
        source.clip = clips[(int)list];
        source.Play();
    }

    public void SetVolumn()
    {
        source.volume = BGMSlider.value;
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Unpause()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void FadeOutMusic()
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

    public void FadeInMusic()
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
