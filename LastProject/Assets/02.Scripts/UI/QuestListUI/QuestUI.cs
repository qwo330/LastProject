using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text.RegularExpressions;

public class QuestUI : MonoBehaviour
{
    [System.Serializable]
    private struct QuestInfo
    {
        public string QuestName;
        public UISprite MonsterImage;
        public UILabel Name, Info, EXP;
        public GameObject CheckedBG, CheckedSprite;
        public GameObject QuestObject;
    }

    [SerializeField]
    private List<QuestInfo> info;

    [SerializeField]
    private GameObject gridStartTab, gridProgressTab, gridCompleteTab;

    [SerializeField]
    private UIScrollView scrollProgressTab, scrollCompleteTab;
    [SerializeField]
    private GameObject questListButton, questProgressButton;

    private Animator listButtonAnim, progressListAnim;

    private List<int> questID = new List<int>();

    [SerializeField]
    private UILabel[] quest = new UILabel[3];

    private int ButtonCount = 0;

    void Start()
    {
        LoadQuestInfoData();
        listButtonAnim = questListButton.GetComponent<Animator>();
        progressListAnim = questProgressButton.GetComponent<Animator>();

    }

    private void LoadQuestInfoData()
    {
        if (File.Exists(Application.dataPath + "/Resources/document.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/document.json");
            JsonData data = JsonMapper.ToObject(jsonStr);

            for (int i = 0; i < data.Count; i++)
            {
                questID.Add(((int)data[i]["ID"]));
                info[i].MonsterImage.spriteName = data[i]["Image1"].ToString();
                info[i].Name.text = data[i]["Name"].ToString();
                info[i].Info.text = data[i]["Description"].ToString();
                info[i].EXP.text = data[i]["EXP"].ToString();
            }
        }
    }

    public int TestID; //NPC 클래스에서 퀘스트 아이디를 넘겨준다. 임시로 인스펙터에서 값을 넣어줌.

    //겟 컴포넌트 뺄 방법을 생각해보자.
    public void OnClickedOngoingTest()
    {
        bool ID = CheckQuestID();
        if (ID)
        {
            info[TestID].QuestObject.transform.SetParent(gridProgressTab.transform);
            gridStartTab.GetComponent<UIGrid>().enabled = true;
            gridProgressTab.GetComponent<UIGrid>().enabled = true;
            info[TestID].QuestObject.GetComponent<UIDragScrollView>().scrollView = scrollProgressTab;
            info[TestID].CheckedBG.active = true;
            info[TestID].QuestObject.GetComponent<UIButton>().enabled = true;
        }
    }

    public void OnClickedComplete()
    {
        bool ID = CheckQuestID();
        if (ID)
        {
            info[TestID].QuestObject.transform.SetParent(gridCompleteTab.transform);
            gridProgressTab.GetComponent<UIGrid>().enabled = true;
            gridCompleteTab.GetComponent<UIGrid>().enabled = true;
            info[TestID].QuestObject.GetComponent<UIDragScrollView>().scrollView = scrollCompleteTab;
            info[TestID].CheckedBG.active = false;
            info[TestID].QuestObject.GetComponent<UIButton>().enabled = false;
        }
    }

    private bool CheckQuestID()
    {
        for (int i = 0; i < questID.Count; i++)
        {
            if (TestID == questID[i])
                return true;
        }
        return false;
    }

    public void OnClickedCheckButton(GameObject value)
    {
        string objectName = value.name;
        string tempName = Regex.Replace(objectName, @"\D", "");
        int buttonNumber = int.Parse(tempName);

        if (! info[buttonNumber].CheckedSprite.active && ButtonCount < 3) 
        {
            info[buttonNumber].CheckedSprite.active = true;
            quest[ButtonCount].text = info[buttonNumber].Info.text; //버튼카운트 값으로 넣어줘야 첫번쨰 목록부터 들어온다
            ButtonCount++;
        }
        else if (info[buttonNumber].CheckedSprite.active)
        {
            info[buttonNumber].CheckedSprite.active = false;
            ButtonCount--;
            quest[ButtonCount].text = null;
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
