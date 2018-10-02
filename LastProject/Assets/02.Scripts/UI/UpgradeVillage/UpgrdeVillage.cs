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

    private int money;

    public void OnClickedConfirmButton()
    {
        money = int.Parse(inputField.text);
        inputField.text = null;
    }

    //플레이어가 투자한 금액을 리턴하는 함수.
    public int GetInvestedMoney()
    {
        return money;
    }
}
