using UnityEngine;

public delegate void TimerCallback();

public class GameTimer : MonoBehaviour
{
    public TimerCallback Callback;

    float callTime, elapseTime;
    bool isWorking = false;
    bool isRepeat = false;

    public void SetTimer(float time = 1f, bool isRepeat = false)
    {
        callTime = time;
        this.isRepeat = isRepeat;
    }

    public void StartTimer()
    {
        isWorking = true;
    }

    public void SleepTimer()
    {
        isWorking = !isWorking;
    }

    public void StopTimer()
    {
        isWorking = false;
        elapseTime = 0f;
    }

    public void ReturnTimer()
    {
        TimerManager.Instance.ReturnTimer(this);
    }

    public float GetRemainTime()
    {
        return callTime - elapseTime;
    }

    private void Update()
    {
        if (isWorking)
        {
            if (elapseTime < callTime)
                elapseTime += TimerManager.Instance.DeltaTime;

            else
            {
                if (isRepeat) elapseTime = 0;
                else StopTimer();

                Callback();
            }
        }
    }
}