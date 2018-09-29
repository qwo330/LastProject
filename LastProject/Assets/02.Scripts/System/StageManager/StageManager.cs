using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public enum SceneChangeType
{
    Town1,
    Town2,
    Field1,
}

public class StageManager : Singleton<StageManager> {
    Vector3[][] playerStartPoint; // [start][end]
	
    public void MapChange()
    {

    }
}
