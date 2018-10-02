using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class NPC : MonoBehaviour {

    private int questID;

    [SerializeField]
    private Text dialogueText;
    private List<int> dialogueID = new List<int>();
    private List<string> scripts = new List<string>();
    private int scriptIndex = 0;

    [SerializeField]
    private Button prev, next, acceptance, cancel;
    [SerializeField]
    private GameObject dialogueUI;

    //private List<int> acceptedList = new List<int>();

    QuestUI quest;
    private void Start()
    {
        LoadDialogue();
    }

    public void OnClickedTestButton(int ID)
    {
        ShowDialogue(ID);
        questID = ID;
    }

    private void ShowDialogue(int ID)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["DialogueId"].ToString() == ID.ToString())
            {
              scripts.Add(data[i]["Scripts"].ToString());
            }
        }
        dialogueText.text = scripts[0];
    }

    private JsonData data;
    private void LoadDialogue()
    {
        if (File.Exists(Application.dataPath + "/Resources/DialogueTest.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/DialogueTest.json");
            data = JsonMapper.ToObject(jsonStr);
        }
    }

    //플레이어에게 퀘스트 아이디를 넘겨 몇번째 퀘스트를 하고 있는지 전달해야한다.
    public void OnClickedNextButton()
    {
        if (scriptIndex < scripts.Count-1)
        {
            scriptIndex++;
            dialogueText.text = scripts[scriptIndex];
        }
        else
        {
            prev.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
            acceptance.gameObject.SetActive(true);
            cancel.gameObject.SetActive(true);
        }
    }

    public void OnClickedPrevButton()
    {
        if (scriptIndex > 0)
        {
            scriptIndex--;
            dialogueText.text = scripts[scriptIndex];
        }
        else
        {
            scriptIndex = 0;
        }
    }

    public void OnClickedAcceptance()
    {
        dialogueUI.gameObject.SetActive(false);
        //SetOngoingQuest(questID);
        GetQuestID(questID);
    }

    //public void SetOngoingQuest(int questId)
    //{
    //    for (int i = 0; i < data.Count; i++)
    //    {
    //        if (data[i]["DialogueId"].ToString() == questId.ToString())
    //        {
    //            acceptedList.Add(questId);
    //            return;
    //        }
    //    }
    //}

    public void OnClickedCancel()
    {
        dialogueUI.gameObject.SetActive(false);
    }

    //수락한 퀘스트의 ID를 가져가는 함수
    public int GetQuestID(int ID)
    {
        Debug.Log("퀘스트아이디를 얻다"+ID);
        return ID;
    }

    //수락한 퀘스트의 리스트를 가져가는 함수
    //public List<int> GetAcceptedList()
    //{
    //    return acceptedList;
    //}

}
