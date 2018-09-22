using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager> {

    public float TimeScale;
    public float DeltaTime;
    float startTime;
    float fixedDeltaTime;

    [SerializeField]
    int timerCount = 40;

    Queue<GameTimer> timerPool;

    [SerializeField]
    GameTimer timerPrefab;

    private void Awake()
    {
        init();
    }

    void init()
    {
        startTime = Time.realtimeSinceStartup;
        fixedDeltaTime = Time.fixedDeltaTime;
        TimeScale = 1f;

        timerPool = new Queue<GameTimer>();
        for (int i = 0; i < timerCount; i++)
        {
            GameTimer timer = Instantiate(timerPrefab, transform);
            timerPool.Enqueue(timer);
        }
    }

    void Update()
    {
        DeltaTime = Time.realtimeSinceStartup - startTime;
        startTime = Time.realtimeSinceStartup;

        Time.fixedDeltaTime = fixedDeltaTime * TimeScale;
    }

    public GameTimer GetTimer()
    {
        GameTimer timer = timerPool.Dequeue();
        return timer;
    }

    public void ReturnTimer(GameTimer timer)
    {
        timerPool.Enqueue(timer);
    }
}
