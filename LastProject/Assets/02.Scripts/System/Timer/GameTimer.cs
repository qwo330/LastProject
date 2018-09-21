using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TimerCallback();

public class GameTimer : MonoBehaviour {
    public TimerCallback Callback;

    public void SetTimer(float time)
    {

    }
}
