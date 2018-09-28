using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text.RegularExpressions;

public class QuestUI : MonoBehaviour
{
    /// <summary>
    /// 퀘스트 프리팹을 동적으로 생성하고 컴포넌트도 동적으로 추가하다.
    /// </summary>
     
    [SerializeField]
    private GameObject grid;
    [SerializeField]
    private GameObject itemPrefab, Name, Info, EXP, Image;

    [SerializeField]
    private GameObject questButton, ListButton;
    private Animator listButtonAnim, progressListAnim;

    [SerializeField]
    private UILabel[] selectedQuestList = new UILabel[3];

    private int ButtonCount = 0;

    void Start()
    {
        LoadQuestInfoData();
         
        listButtonAnim = questButton.GetComponent<Animator>();
        progressListAnim = ListButton.GetComponent<Animator>();
    }

    private void AddQuest(string name, string info, string exp, string spriteName)
    {
        GameObject obj = grid.AddChild(itemPrefab); 

        GameObject questName = obj.AddChild(Name);
        questName.transform.localPosition = new Vector3(-97, 27, 0);
        questName.GetComponent<UILabel>().text = name;
       
        GameObject questInfo = obj.AddChild(Info);
        questInfo.GetComponent<UILabel>().text = info;

        GameObject experience = obj.AddChild(EXP);
        experience.transform.localPosition = new Vector3(-110,-29,0);
        experience.GetComponent<UILabel>().text = exp;

        GameObject questImage = obj.AddChild(Image);
        questImage.transform.localPosition = new Vector3(-196,0,0);
        questImage.GetComponent<UISprite>().spriteName = spriteName;
    }
 
    private void LoadQuestInfoData()
    {
        if (File.Exists(Application.dataPath + "/Resources/document.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/document.json");
            JsonData data = JsonMapper.ToObject(jsonStr);

            for (int i = 0; i < data.Count; i++)
            {
                string name = data[i]["Name"].ToString();
                string info = data[i]["Description"].ToString();
                string exp = data[i]["EXP"].ToString();
                string spriteName = data[i]["Image1"].ToString();

                AddQuest(name, info, exp, spriteName);
            }
        }
    }

    public int TestID; //NPC 클래스에서 퀘스트 아이디를 넘겨준다. 임시로 인스펙터에서 값을 넣어줌.

    public void OnClickedCheckButton(GameObject value)
    {
        string objectName = value.name;
        string tempName = Regex.Replace(objectName, @"\D", "");
        int buttonNumber = int.Parse(tempName);

        if (ButtonCount < 3)
        {
            /* questList[ButtonCount].text = info[buttonNumber].Info.text; *///버튼카운트 값으로 넣어줘야 첫번쨰 목록부터 들어온다
            ButtonCount++;
        }
        else
        {
            ButtonCount--;
            selectedQuestList[ButtonCount].text = null;
        }
    }

    //퀘스트 진행상황 창을 열고 닫는 함수
    public void OnClickedQuestListIcon()
    {
        listButtonAnim.SetTrigger("ButtonHide");
        progressListAnim.SetTrigger("ListShow");
    }

    public void OnClickedQuestProgress()
    {
        progressListAnim.SetTrigger("ListHide");
        listButtonAnim.SetTrigger("ButtonShow");

    }
}
