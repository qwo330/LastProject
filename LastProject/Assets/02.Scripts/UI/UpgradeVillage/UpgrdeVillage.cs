using UnityEngine;
using UnityEngine.UI;

public class UpgrdeVillage : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

    public enum VillageType
    {
        Forest,
        Snow,
    }
    public VillageType type;

    private int forestVillageMoney;
    private int snowVillageMoney;

    public void OnClickedConfirmButton()
    {
        if (type == VillageType.Forest)
        {
            forestVillageMoney = int.Parse(inputField.text);
        }
        else if(type == VillageType.Snow)
        {
            snowVillageMoney = int.Parse(inputField.text);
        }
        
        inputField.text = null;
    }

    //플레이어가 투자한 금액을 리턴하는 함수.
    public int GetInvestedMoney()
    {
        if (type == VillageType.Forest)
        {
            return forestVillageMoney;
        }
        else 
        {
            return snowVillageMoney; 
        }
    }
}
