using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class NPC : MonoBehaviour {

    //버튼을 눌렀을때 퀘스트 아이디가 전송된다.
    public int questId;

    [SerializeField]
    private Text dialogueText;
    private List<int> dialogueID = new List<int>();
    private List<string> scripts = new List<string>();

    [SerializeField]
    private Button prev, next, acceptance, confirm;
    [SerializeField]
    private GameObject dialogueUI;


    private void Start()
    {
        LoadDialogueData();
        dialogueText.text = scripts[0];
    }

    public int GetQuestId
    {
        get { return questId; }
    }

    private void LoadDialogueData()
    {
        if (File.Exists(Application.dataPath + "/Resources/Dialogue.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Dialogue.json");
            JsonData data = JsonMapper.ToObject(jsonStr);

            for (int i = 0; i < data.Count; i++)
            {
                scripts.Add (data[i]["Scripts"].ToString());
                //dialogueID.Add((int)data[i]["DialogueId"]);
            }
        }
    }

    //플레이어에게 퀘스트 아이디를 넘겨 몇번째 퀘스트를 하고 있는지 전달해야한다.
    //플레이어가 다시 찾아왔을때 대사 내용이 바뀌어야함. 
    //플레이어 레벨별로 퀘스트를 진행할 수 있도록 레벨에 따라 설정
    //플레이어가 퀘스트 완료조건을 충족했을시 다른 대사가 떠야한다.
    private int index;

    public void OnClickedNextButton()
    {
        if (index < scripts.Count-1)
        {
            index++;
            dialogueText.text = scripts[index];
        }
        else
        {
            prev.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
            acceptance.gameObject.SetActive(true);
        }
    }

    public void OnClickedPrevButton()
    {
        if (index > 0)
        {
            index--;
            dialogueText.text = scripts[index];
        }
    }

    public void OnClickedAcceptance()
    {
        dialogueUI.gameObject.SetActive(false);
        //퀘스트 아이디 넘겨주기
    }

    

    public void OnClickedConfirm()
    {
        //퀘스트 아이디 넘겨주기
    }

  


}
