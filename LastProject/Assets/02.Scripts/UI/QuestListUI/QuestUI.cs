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
    private GameObject ItemPrefab, Name, Info, EXP, Icon;//Label로 바꾸기.   

    [SerializeField]
    private Animator listButtonAnimator, listPanelAnimator;

    [SerializeField]
    private UILabel[] selectedQuestLabels = new UILabel[3];

    private List<GameObject> QuestList = new List<GameObject>();

    private int ButtonCount = 0;

    void Start()
    {
        EventDelegate.Add(ItemPrefab.GetComponent<UIToggle>().onChange, OnClickedQuestButton);
        LoadQuestInfoData();
    }

    private GameObject AddQuest(string name, string info, string exp, string spriteName)
    {
        GameObject obj = grid.AddChild(ItemPrefab);
        
        GameObject questName = obj.AddChild(Name);
        questName.transform.localPosition = new Vector3(-97, 27, 0);
        questName.GetComponent<UILabel>().text = name;

        GameObject questInfo = obj.AddChild(Info);
        questInfo.GetComponent<UILabel>().text = info;

        GameObject experience = obj.AddChild(EXP);
        experience.transform.localPosition = new Vector3(-110, -29, 0);
        experience.GetComponent<UILabel>().text = exp;

        GameObject questImage = obj.AddChild(Icon);
        questImage.transform.localPosition = new Vector3(-196, 0, 0);
        questImage.GetComponent<UISprite>().spriteName = spriteName;

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

                QuestList.Add(AddQuest(name, info, exp, spriteName));
            }
        }
    }

    public int TestID; //NPC 클래스에서 퀘스트 완료된 아이디를 넘겨준다. 임시로 인스펙터에서 값을 넣어줌.

    public void OnClickedTestButton()
    {
        CompleteQuest(TestID);
    }

    public void CompleteQuest(int TestID)
    {
        //완료된게 몇번째 퀘스트 오브젝트 인지 알아내야함
       
    }

    public void OnClickedQuestButton()
    {
        
        

        
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
