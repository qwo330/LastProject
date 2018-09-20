using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterBookData : MonoBehaviour
{
    public string Icon;
    public string Name;
    public string Description;
    public string Ingredient1;
    public string Ingredient2;
    public string Ingredient3;

    public MonsterBookData(string icon, string name, string description, string ingredient1, string ingredient2, string ingredient3)
    {
        Icon = icon;
        Name = name;
        Description = description;
        Ingredient1 = ingredient1;
        Ingredient2 = ingredient2;
        Ingredient3 = ingredient3;
    }

}
