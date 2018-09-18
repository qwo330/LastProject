using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

[CreateAssetMenu(fileName ="New Monster", menuName ="MonsterBook/Monster")]
public class MonstersInfo : ScriptableObject {

    new public string name = "New Monster";
    public Texture MonsterIcon;
    public enum Type
    {
        Wolf,
        Sheep,
        Rabbit,
        Ent,
    };

    public Type type;
}
