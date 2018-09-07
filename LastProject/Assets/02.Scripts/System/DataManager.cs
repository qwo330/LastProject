using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

// http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=52133
// 참고링크

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

    PlayerData playerInfo;
    TownData[] townInfos;

    /// <summary>
    /// 게임을 시작할 때 필요한 정보들을 Data파일에서 불러온다.
    /// </summary>
    public void LoadGameData()
    {
        if (File.Exists(Application.dataPath + playerDataPath))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + playerDataPath);
            Debug.Log(jsonStr);
            PlayerData a = JsonMapper.ToObject<PlayerData>(jsonStr);

            Debug.Log(a.Level);
            Debug.Log(a.MaxHp);
            Debug.Log(a.Belongings[2].ItemName);

            Debug.Log("PlayerData Load 완료");
        }
        else
            Debug.Log("PlayerData가 존재하지 않습니다.");
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
    public void SavePlayerData()
    {
        if (!Directory.Exists(Application.dataPath + "/LocalData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/LocalData");
            Debug.Log("Local 디렉토리 생성");
        }
        JsonData playerData = JsonMapper.ToJson(playerInfo);
        File.WriteAllText(Application.dataPath + playerDataPath, playerData.ToString());

        Debug.Log("PlayerData Save 완료");
    }

    private void Start()
    {
        playerInfo = new PlayerData();
        playerInfo.Level = 15;
        playerInfo.MaxHp = playerInfo.Hp = 40;
        playerInfo.Belongings = new ItemData[Defines.InventorySize];
        playerInfo.Gold = 15101;
        playerInfo.Belongings[2].ItemName = "woodSword";
        SavePlayerData();

        LoadGameData();
    }

    /// <summary>
    /// 마을과 관련된 정보들을 Data파일에 저장한다.
    /// </summary>
    public void SaveTownData()
    {

    }
}
