using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestNumber
{
    HeadMan = 0,
    ForestFactory = 1,
    ForestShop = 2,
    SnowFactory = 3,
    SnowShop = 4,
}

public class OnClickedObject : MonoBehaviour {
   
    public QuestNumber npcType;

    [SerializeField]
    private Camera camera;
    private Ray ray;
    private RaycastHit hitInfo;

    [SerializeField]
    private GameObject dialogueObject;
    private NPC npc;

    private void Start()
    {
        SetInitial();
    }

    private void SetInitial()
    {
        npc = dialogueObject.GetComponent<NPC>();
        dialogueObject.SetActive(false);//clickedObject값을 가져오기위해 처음에 true였다가 false로 바꿔줌.
        npc.OnClickedNPC((int)npcType); //일반 인트값은 되는데 왜 이넘값은 안되는거지
    }

    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                dialogueObject.SetActive(true);
            }
        }
    }
}
