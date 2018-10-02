using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


public class MonsterBook : MonoBehaviour
{
    public UILabel Name;
    public UILabel Description;
    public UISprite MonsterSprite, Ingredient1, Ingredient2, Ingredient3;

    public enum MonsterNames
    {
        Wolf,
        Rabbit,
        Witch,
        Fox,
        Ghost,
    }
    public MonsterNames monsterNames;

    private enum Ingredient
    {
        Wood,
        Meat,
        Iron,
        Salt,
        Stone
    }

    public void OnClickMonsterButton()
    {
        if (File.Exists(Application.dataPath + "/Resources/MonsterBookData.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/MonsterBookData.json");
            JsonData data = JsonMapper.ToObject(jsonStr);

            ShowMonsterInfo(data);
        }

    }

    private void ShowMonsterInfo(JsonData data)
    {
        for (int i = 0; i < Defines.TotalMonsterCount; i++)
        {
            if (this.monsterNames.ToString() == data[i]["Icon"].ToString())
            {
                Name.text = data[i]["Name"].ToString();
                Description.text = data[i]["Description"].ToString();
                MonsterSprite.spriteName = data[i]["Icon"].ToString();
                Ingredient1.spriteName = data[i]["Ingredient1"].ToString();
                Ingredient2.spriteName = data[i]["Ingredient2"].ToString();
                Ingredient3.spriteName = data[i]["Ingredient3"].ToString();
            }
        }
    }


}
