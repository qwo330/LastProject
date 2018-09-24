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
    private GameObject ongoingTabGrid, completeTabGrid;
    [SerializeField]
    private UIScrollView ongoingTabScroll, completeTabScroll;

    private List<int> questID = new List<int>();

    [SerializeField]
    private UILabel[] quest = new UILabel[3];

    private int ButtonCount = 0;

    void Start()
    {
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
                questID.Add(((int)data[i]["ID"]));
                info[i].MonsterImage.spriteName = data[i]["Image1"].ToString();
                info[i].Name.text = data[i]["Name"].ToString();
                info[i].Info.text = data[i]["Description"].ToString();
                info[i].EXP.text = data[i]["EXP"].ToString();
            }
        }
    }

    //private NPC npc;
    public int TestID; //NPC 클래스에서 퀘스트 아이디를 넘겨준다.

    public void OnClickedOngoingTest()
    {
        bool ID = CheckQuestID();
        if (ID)
        {
            info[TestID].QuestObject.transform.SetParent(ongoingTabGrid.transform);
            info[TestID].QuestObject.GetComponent<UIDragScrollView>().scrollView = ongoingTabScroll;
            info[TestID].CheckedBG.active = true;
            info[TestID].QuestObject.GetComponent<UIButton>().enabled = true;
        }
    }


    public void OnClickedComplete()
    {
        bool ID = CheckQuestID();
        if (ID)
        {
            info[TestID].QuestObject.transform.SetParent(completeTabGrid.transform);
            info[TestID].QuestObject.GetComponent<UIDragScrollView>().scrollView = completeTabScroll;
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

        if (!info[buttonNumber].CheckedSprite.active && ButtonCount < 3)
        {
            info[buttonNumber].CheckedSprite.active = true;
            quest[buttonNumber].text = info[buttonNumber].Info.text;
            ButtonCount++;
        }
        else if (info[buttonNumber].CheckedSprite.active)
        {
            info[buttonNumber].CheckedSprite.active = false;
            ButtonCount--;
            quest[buttonNumber].text = null;
        }
    }
}
