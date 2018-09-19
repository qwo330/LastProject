using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBook : MonoBehaviour
{

    public UILabel Name;

    public UILabel Description;

    public UISprite MonsterSprite;

   
    public enum MonsterNames
    {
        Wolf,
        Rabbit,
        Witch,
        Fox,
        Ghost
    }

    public MonsterNames SelectName;



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

                int.TryParse(row[0], out q.ID);
                q.Name = row[1];
                q.Description = row[2];

                quests.Add(q);
            }
        }
    }

    public void OnClickMonsterButton()
    {
        for (int i = 0; i < Defines.TotalMonsterCount; i++)
        {
            if ((int)this.SelectName == i)
            {
                Name.text = quests[i].Name;
                Description.text = quests[i].Description;
                MonsterSprite.spriteName = ((MonsterNames)i).ToString();
            }
        }
    }
}
