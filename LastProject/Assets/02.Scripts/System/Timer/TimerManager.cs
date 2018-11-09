using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager> {

    public float TimeScale;
    public float DeltaTime { get; private set; }
    float startTime;
    float fixedDeltaTime;

    [SerializeField]
    int timerCount = 128;

    Queue<GameTimer> timerPool;

    GameTimer timerPrefab;

    public void Init()
    {
        timerPrefab = (Resources.Load("Prefabs/GameTimer") as GameObject).GetComponent<GameTimer>();

        startTime = Time.realtimeSinceStartup;
        fixedDeltaTime = Time.fixedDeltaTime;
        TimeScale = 1f;

        timerPool = new Queue<GameTimer>();
        for (int i = 0; i < timerCount; i++)
        {
            GameTimer timer = Instantiate(timerPrefab, transform);
            timerPool.Enqueue(timer);
            timer.gameObject.SetActive(false);
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
        timer.gameObject.SetActive(true);
        return timer;
    }

    public void ReturnTimer(GameTimer timer)
    {
        timerPool.Enqueue(timer);
        timer.gameObject.SetActive(false);
    }
}
