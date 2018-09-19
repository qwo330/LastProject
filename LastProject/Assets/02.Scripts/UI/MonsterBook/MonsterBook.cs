using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    List<MonsterBookDataList> quests = new List<MonsterBookDataList>();

    void Start()
    {
        TextAsset questdata = Resources.Load<TextAsset>("MonsterBookData");

        string[] data = questdata.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row[1] != "")
            {
                MonsterBookDataList q = new MonsterBookDataList();

                q.Icon = row[0];
                q.Name = row[1];
                q.Description = row[2];
                q.Ingredient1 = row[3];
                q.Ingredient2 = row[4];
                q.Ingredient3 = row[5];

                quests.Add(q);
            }
        }
    }

    public void OnClickMonsterButton()
    {
        for (int i = 0; i < Defines.TotalMonsterCount; i++)
        {
            if (this.monsterNames.ToString() == quests[i].Icon)
            {
                Name.text = quests[i].Name;
                Description.text = quests[i].Description;
                MonsterSprite.spriteName = quests[i].Icon;
                Ingredient1.spriteName = quests[i].Ingredient1;
                Ingredient2.spriteName = quests[i].Ingredient2;
                Ingredient3.spriteName = quests[i].Ingredient3; //왜 \r이 들어가지?
            }
        }
    }

}
