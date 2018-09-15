using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] UIGrid EpuipmentWindowGrid;
    [SerializeField] UIGrid PotionWindowGrid;
    [SerializeField] UIGrid NeedItemWindowGrid;
    [SerializeField] GameObject CraftSlot;
    [SerializeField] GameObject NeedSlot;

    //각 탭별로 제작가능한 아이템의 개수
    int MaxEquipItemCount = 10;
    int MaxPotionItemCount = 3;
    const int MaxNeedItemCount = 4;

    List<CraftSlot> EquipList;
    List<CraftSlot> PotionList;
    List<UISprite> NeedList;

    private void Start()
    {
        //제작 가능한 리스트 얻어오기
        //MaxItemCount = 

        if (CraftSlot != null)
        {
            CreateList(EpuipmentWindowGrid, EquipList, MaxEquipItemCount, CraftSlot);
            CreateList(PotionWindowGrid, PotionList, MaxPotionItemCount, CraftSlot);
            CreateList(NeedItemWindowGrid, NeedList, MaxNeedItemCount, NeedSlot);
        }
    }

    void CreateList<T>(UIGrid windowGrid, List<T> list, int count, GameObject slot)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject item = NGUITools.AddChild(windowGrid.gameObject, slot);

            list = new List<T>(count)
            {
                item.GetComponent<T>()
            };

            //제작 가능한 리스트 정보를 리스트랑 매칭시킨다.
            //내용이 없는 리스트는 비활성화
        }

        windowGrid.Reposition();
    }


}
