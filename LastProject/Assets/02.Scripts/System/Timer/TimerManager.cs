using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager> {

    public float TimeScale;
    public float DeltaTime;
    float startTime;
    float fixedDeltaTime;

    Queue<GameTimer> TimerPool;

    void init()
    {
        TimeScale = 1f;
        
    }

    void Update()
    {
        DeltaTime = Time.realtimeSinceStartup - startTime;
        startTime = Time.realtimeSinceStartup;

        Time.fixedDeltaTime = fixedDeltaTime * TimeScale;
    }
}
