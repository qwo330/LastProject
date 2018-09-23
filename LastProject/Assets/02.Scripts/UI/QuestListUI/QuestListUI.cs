using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class QuestListUI : MonoBehaviour {

	void Start () {
        LoadQuestInfoData();
    }

    private void LoadQuestInfoData()
    {
        if (File.Exists(Application.dataPath + "/Resources/document.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/document.json");
            JsonData data = JsonMapper.ToObject(jsonStr);
            for (int i = 0; i < data.Count; i++)
            {
               Debug.Log( data[i]["Name"]);
            }
            
        }

    }
    

    /*
    private void ShowMonsterInfo(JsonData data)
    {
        for (int i = 0; i < Defines.TotalMonsterCount; i++)
        {
            if (this.monsterNames.ToString() == data[i]["Icon"].ToString())
            {
                Debug.Log("asdfsf");
                Name.text = data[i]["Name"].ToString();
                Description.text = data[i]["Description"].ToString();
                MonsterSprite.spriteName = data[i]["Icon"].ToString();
                Ingredient1.spriteName = data[i]["Ingredient1"].ToString();
                Ingredient2.spriteName = data[i]["Ingredient2"].ToString();
                Ingredient3.spriteName = data[i]["Ingredient3"].ToString();
            }
        }
    }
    */
}
