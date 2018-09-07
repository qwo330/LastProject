using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public struct PlayerData
{
    public int Level;
    public int Hp, MaxHp;
    public int Gold;
    public ItemData[] Belongings;
}

public struct TownData
{
    public string TownName;
    public int TownLevel;
    public int PlayerContribution;
    // + 마을에 지어진 건물 정보, 다른 마을과 연결된 통행로 등 
}

public class DataManager : MonoBehaviour {
    const string playerDataPath = "/LocalData/PlayerData.json";
    const string townDataPath = "/LocalData/TownData.json";
    const string systemDataPath = "/LocalData/SystemData.json";

    /// <summary>
    /// 게임을 시작할 때 필요한 정보들을 Data파일에서 불러온다.
    /// </summary>
    public void LoadGameData()
    {

    }


    /// <summary>
    /// 게임 설정과 관련된 정보들을 Data파일에 저장한다.
    /// ex) 사운드 크기 등
    /// </summary>
    public void SettingData()
    {
        
    }

    /// <summary>
    /// 플레이어와 관련된 정보들을 Data파일에 저장한다.
    /// </summary>
    public void SavePlayerData(PlayerData playerInfo)
    {
        if (!Directory.Exists(Application.dataPath + "/LocalData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/LocalData");
            Debug.Log("Local 디렉토리 생성");
        }
        JsonData playerData = JsonMapper.ToJson(playerInfo);
        File.WriteAllText(Application.dataPath + playerDataPath, playerData.ToString());
        Debug.Log("플레이어 데이터 저장 완료");
    }
    PlayerData a;
    private void Start()
    {
        a = new PlayerData();
        a.Level = 15;
        a.MaxHp = a.Hp = 40;
        a.Belongings = new ItemData[Defines.InventorySize];
        a.Gold = 15101;
        SavePlayerData(a);
    }

    /// <summary>
    /// 마을과 관련된 정보들을 Data파일에 저장한다.
    /// </summary>
    public void SaveTownData(TownData[] townInfos)
    {

    }
	
}
