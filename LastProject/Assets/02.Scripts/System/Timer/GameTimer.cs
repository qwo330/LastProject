using UnityEngine;

public delegate void TimerCallback();

public class GameTimer : MonoBehaviour{
    public TimerCallback Callback;

    public float callTime;
    bool isWorking = false;

    public void SetTimer(float time = 1)
    {
        callTime = time;
    }

    public void StartTimer()
    {
        isWorking = true;
    }

    public void SleepTimer()
    {
        isWorking = false;
    }

    public void StopTimer()
    {
        isWorking = false;
    }

    public void ReturnTimer()
    {
        TimerManager.Instance.ReturnTimer(this);
    }

    public float GetRemainTime()
    {
        return callTime;
    }

    private void Update()
    {
        if(isWorking)
        {
            if (callTime > 0)
                callTime -= TimerManager.Instance.DeltaTime;
            else
            {
                StopTimer();
                Callback();
            }
        }
    }
}
