using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BGM
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



public class BGMManager : MonoBehaviour {

    static public BGMManager instance;
    [SerializeField]
    public BGM[] clips; //배경음악들
    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);

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

    void Start () {
        for (int i = 0; i < clips.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + "=" + clips[i].Name);
            clips[i].Setsource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
        source = GetComponent<AudioSource>();
		
	}

    public void Play(string name)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (name == clips[i].Name)
            {
                clips[i].Play();
                return;
            }
        }
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
        for (float i = 1.0f; i >= 0 ; i-=0.01f)
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
        for (float i = 0; i <= 1; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void SetLoop(string name)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (name == clips[i].Name)
            {
                clips[i].SetLoop();
                return;
            }
        }
    }


    public void SetLoopCancel(string name)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (name == clips[i].Name)
            {
                clips[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetBGMVolume(float voluem)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].Volume = voluem;
            clips[i].SetVolume();
        }
    }
}
