using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    //버튼을 눌렀을때 퀘스트 아이디가 전송된다.
    public int questId;
    
    public int GetQuestId 
    {
        get { return questId; }
    }


}
