using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class NPC : MonoBehaviour {
    [HideInInspector]
    public int questID;

    [SerializeField]
    private Text dialogueText;
    private List<int> dialogueID = new List<int>();
    private List<string> scripts = new List<string>();
    private int scriptIndex = 0;

    [SerializeField]
    private Button prev, next, acceptance, cancel;
    [SerializeField]
    private GameObject dialogueUI;
  
    QuestUI quest;

    private void Awake()
    {
        LoadDialogue();
    }
    
    //NPC에서 호출
    public void OnClickedNPC(int ID)
    {
        ShowDialogue(ID);
        questID = (ID);
        
    }

    public  void ShowDialogue(int ID)
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
        if (File.Exists(Application.dataPath + "/Resources/Dialogue.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Dialogue.json");
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
        GetQuestID(questID);

        //스크립트 값 초기화
        scripts.Clear();
        scriptIndex = 0;

        //버튼 초기화
        prev.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        acceptance.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
    }

    public void OnClickedCancel()
    {
        dialogueUI.gameObject.SetActive(false);

        //스크립트 값 초기화
        scripts.Clear();
        scriptIndex = 0;

        //버튼 초기화
        prev.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        acceptance.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);

        
    }

    //수락한 퀘스트의 ID를 가져가는 함수
    public int GetQuestID(int ID)
    {
        Debug.Log("퀘스트아이디를 얻다"+ID);
        return ID;
    }

}
