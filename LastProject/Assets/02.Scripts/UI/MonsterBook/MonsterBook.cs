using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBook : MonoBehaviour
{

    public UILabel Name;

    public UILabel Description;

   
    public enum MonsterNames
    {
        Wolf,
        Rabbit,
        Witch,
        Fox,
        Ghost
    }

    public MonsterNames SelectName;

    ////////////////////


    List<Quest> quests = new List<Quest>();

    // Use this for initialization
    void Start()
    {
        TextAsset questdata = Resources.Load<TextAsset>("questdata2");

        string[] data = questdata.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row[1] != "")
            {
                Quest q = new Quest();

                int.TryParse(row[0], out q.id);
                q.name = row[1];
                q.npc = row[2];
                q.desc = row[3];
                int.TryParse(row[4], out q.status);
                q.rewards = row[5];
                q.task = row[6];
                int.TryParse(row[7], out q.parent);

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
                Name.text = quests[i].id.ToString();
                Description.text = quests[i].desc;
            }
        }

    }
}
