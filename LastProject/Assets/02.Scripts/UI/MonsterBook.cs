using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBook : MonoBehaviour {

    public enum MonsterNames
    {
        Wolf,
        Rabbit,
        Witch,
        Fox,
        Ghost
    }

    public MonsterNames SelectName;

    public void OnClickMonsterButton()
    {
        for (int i = 0; i < Defines.TotalMonsterCount; i++)
        {
            if (this.SelectName == (MonsterNames)i)
            {
                Debug.Log((MonsterNames)i);
            }
        }
       
    }
}
