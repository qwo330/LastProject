using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NPCType
{
    Headman = 0,
    ForestFactory = 1,
    ForestShop = 2,
    SnowFactory = 3,
    SnowShop = 4,
}

public class OnClickedNPC : MonoBehaviour {

    public NPCType type;

    [SerializeField]
    private Camera camera;
    private Ray ray;
    private RaycastHit hitInfo;

    [SerializeField]
    private GameObject dialogueObject;
    [SerializeField]
    private GameObject upgradeObject;
    [SerializeField]
    private GameObject headmanObj, ForestFactoryObj;
    [SerializeField]
    private Button acceptButton;

    private NPC npc;

    private void Start()
    {
        SetInitial();
    }

    private void SetInitial()
    {
        npc = dialogueObject.GetComponent<NPC>();
        dialogueObject.SetActive(false);//clickedObject값을 가져오기위해 처음에 true였다가 false로 바꿔줌.
    }

   
    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                    ShowUI();
            }
        }
    }

   
    private void ShowUI()
    {

        if (hitInfo.collider.gameObject.Equals(headmanObj))
        {
            npc.OnClickedNPC((int)NPCType.Headman);
            dialogueObject.SetActive(true);
            acceptButton.onClick.AddListener(delegate () { upgradeObject.SetActive(true); });
            Debug.Log("Headman");
        }
        else if (hitInfo.collider.gameObject.Equals(ForestFactoryObj))
        {
            npc.OnClickedNPC((int)NPCType.ForestFactory);
            dialogueObject.SetActive(true);
                acceptButton.onClick.AddListener(delegate () { upgradeObject.SetActive(false); });
            Debug.Log("another");
        }

        

    }

}
