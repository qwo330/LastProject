using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCaller : MonoBehaviour {

    private void Awake()
    {
        StageManager.Instance.Init();
    }
}
