using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text.RegularExpressions;

public class QuestUI : MonoBehaviour
{
    [SerializeField]
    private GameObject grid;
    [SerializeField]
    private GameObject questPrefab, nameObj, infoObj, EXPObj /*iconObj*/;

    [SerializeField]
    private Animator listButtonAnimator, listPanelAnimator;

    [SerializeField]
    private UILabel[] selectedQuest = new UILabel[3];

    private List<GameObject> questList = new List<GameObject>();
    private List<UIToggle> questToggle = new List<UIToggle>();


    void Init()
    {
        EventDelegate.Add(questPrefab.GetComponent<UIToggle>().onChange, OnClickedQuestButton);
        LoadQuestInfoData();
        grid.GetComponent<UIGrid>().enabled = true;
        for (int i = 0; i < questList.Count; i++)
        {
            questToggle.Add(questList[i].GetComponent<UIToggle>());
        }

    }

    private GameObject AddQuest(string name, string info, string exp, string spriteName, int ID)
    {
        GameObject obj = grid.AddChild(questPrefab);

        GameObject questName = obj.AddChild(nameObj);
        questName.transform.localPosition = new Vector3(-97, 27, 0);
        questName.GetComponent<UILabel>().text = name;

        GameObject questInfo = obj.AddChild(infoObj);
        questInfo.GetComponent<UILabel>().text = info;

        GameObject experience = obj.AddChild(EXPObj);
        experience.transform.localPosition = new Vector3(-110, -29, 0);
        experience.GetComponent<UILabel>().text = exp;

        //GameObject questImage = obj.AddChild(iconObj);
        //questImage.transform.localPosition = new Vector3(-196, 0, 0);
        //questImage.GetComponent<UISprite>().spriteName = spriteName;

        int qusetID = ID;

        return obj;
    }

    private string jsonStr;
    private JsonData data;
    private void LoadQuestInfoData()
    {
        if (File.Exists(Application.dataPath + "/Resources/document.json"))
        {
            jsonStr = File.ReadAllText(Application.dataPath + "/Resources/document.json");
            data = JsonMapper.ToObject(jsonStr);

            for (int i = 0; i < data.Count; i++)
            {
                string name = data[i]["Name"].ToString();
                string info = data[i]["Description"].ToString();
                string exp = data[i]["EXP"].ToString();
                string spriteName = data[i]["Image1"].ToString();

                questList.Add(AddQuest(name, info, exp, spriteName, i));
            }
        }
    }

    public int TestID; //NPC 클래스에서 퀘스트 완료된 아이디를 넘겨준다. 임시로 인스펙터에서 값을 넣어줌.

    public void OnClickedTestButton()   //다른 곳에서 사용할때 이렇게 사용하면 됨.
    {
        CompleteQuest(TestID);
    }

    public void CompleteQuest(int TestID)   //나중에 다른곳에서 사용하기 위해  public으로 처리함
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (i == TestID)
            {
                questList[i].GetComponent<UISprite>().color = new Color(0.5f,0.5f,0.5f);
                questList[i].GetComponent<UIToggle>().value = false;
                questList[i].GetComponent<UIToggle>().enabled = false;
                questList[0].transform.SetAsLastSibling();
                grid.GetComponent<UIGrid>().enabled = true;
            } 
        }

    }

    public void OnClickedQuestButton()
    {
        for (int i = 0; i < questToggle.Count; i++)
        {
            if (questToggle[i].value == true && selectedQuest[i].text == "")
            {
                selectedQuest[i % selectedQuest.Length].text = data[i]["Description"].ToString();
            }

            if (questToggle[i].value == false && selectedQuest[i].text != "")
            {
                selectedQuest[i].text = "";
            }
        }
        Debug.Log("Test");
    }

    //퀘스트 진행상황 창을 열고 닫는 함수
    public void OnClickedListButton()
    {
        listButtonAnimator.SetTrigger("ButtonHide");
        listPanelAnimator.SetTrigger("ListShow");
    }

    public void OnClickedListPanel()
    {
        listButtonAnimator.SetTrigger("ButtonShow");
        listPanelAnimator.SetTrigger("ListHide");
    }
}
