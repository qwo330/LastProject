using UnityEngine;

public delegate void TimerCallback();

public class GameTimer : MonoBehaviour {
    public TimerCallback Callback;
    float callTime;
    bool isWorking = false;

    public void SetTimer(float time)
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
            if(callTime < 0) callTime -= TimerManager.Instance.DeltaTime;
            else
            {
                Callback();
                StopTimer();
            }
        }
    }
}
