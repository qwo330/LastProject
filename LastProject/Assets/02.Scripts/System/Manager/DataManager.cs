﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

// http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=52133
// 참고링크

public struct PlayerData
{
    public int Attack;
    public int Defense;
    public int Health, MaxHealth;
    public int Level;
    public int Exp;
    public ItemData[] Equipments;
    public ItemData[] Belongings;

    public PlayerData(int i = 0)
    {
        Attack = 10;
        Defense = 10;
        MaxHealth = Health = 100;

        Level = 1;
        Exp = 0;

        Equipments = new ItemData[4];
        Belongings = new ItemData[30];
    }
}

public struct TownData
{
    public string TownName;
    public int TownLevel;
    public int PlayerContribution;

    public TownData(string townName)
    {
        TownName = townName;
        TownLevel = 1;
        PlayerContribution = 0;
    }
    // + 마을에 지어진 건물 정보, 다른 마을과 연결된 통행로 등 
}

public struct SystemData
{
    public float SFXVolume;
    public float BGMVolume;

    public SystemData(float volume = .7f)
    {
        SFXVolume = volume;
        BGMVolume = volume;
    }
}

public class DataManager : Singleton<DataManager> {
    const string playerDataPath = "/LocalData/PlayerData.json";
    const string townDataPath = "/LocalData/TownData.json";
    const string systemDataPath = "/LocalData/SystemData.json";

    PlayerData playerInfo;
    TownData[] townInfos;
    SystemData systemSetting;

    public void Init()
    {
        //LoadJsonData();
    }

    /// <summary>
    /// 게임을 시작할 때 필요한 정보들을 Data파일에서 불러온다.
    /// </summary>
    public void LoadJsonData()
    {
        LoadPlayerData();
        LoadTownData();
        LoadSystemData();
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

    public void LoadPlayerData()
    {
        if (File.Exists(Application.dataPath + playerDataPath))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + playerDataPath);
            Debug.Log(jsonStr);
            PlayerData data = JsonMapper.ToObject<PlayerData>(jsonStr);

            Debug.Log(data.Level);
            Debug.Log(data.MaxHealth);
            Debug.Log(data.Belongings[2].ItemName);

            Debug.Log("PlayerData Load 완료");
        }
        else
        {
            Debug.Log("PlayerData가 존재하지 않습니다.");

            playerInfo = new PlayerData();

            Debug.Log("PlayerData를 생성하였습니다.");
        }
    }

    public void SetPlayerData()
    {

    }

    public PlayerData GetPlayerData()
    {
        return playerInfo;
    }

    /// <summary>
    /// 마을과 관련된 정보들을 Data파일에 저장한다.
    /// </summary>
    public void SaveTownData()
    {

    }

    public void LoadTownData()
    {
        townInfos = new TownData[Defines.TownCount];

        if (File.Exists(Application.dataPath + townDataPath))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + townDataPath);
            Debug.Log(jsonStr);
            TownData data = JsonMapper.ToObject<TownData>(jsonStr);


            Debug.Log("TownData Load 완료");
        }
        else
        {
            Debug.Log("TownData가 존재하지 않습니다.");

            townInfos[0] = new TownData("ForestVillage");
            townInfos[1] = new TownData("SnowVillage");

            Debug.Log("TownData를 생성하였습니다.");
        }
    }

    public void SetTownData()
    {

    }

    public TownData GetTownData(int townCode)
    {
        return townInfos[townCode];
    }

    /// <summary>
    /// 게임 설정과 관련된 정보들을 Data파일에 저장한다.
    /// ex) 사운드 크기 등
    /// </summary>
    public void SaveSystemData()
    {
        if (!Directory.Exists(Application.dataPath + "/LocalData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/LocalData");
            Debug.Log("Local 디렉토리 생성");
        }
        JsonData SystemData = JsonMapper.ToJson(systemSetting);
        File.WriteAllText(Application.dataPath + systemDataPath, SystemData.ToString());

        Debug.Log("SystemData Save 완료");
    }

    public void LoadSystemData()
    {
        if (File.Exists(Application.dataPath + systemDataPath))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + systemDataPath);
            Debug.Log(jsonStr);
            SystemData data = JsonMapper.ToObject<SystemData>(jsonStr);


            Debug.Log("SystemData Load 완료");
        }
        else
        {
            Debug.Log("SystemData가 존재하지 않습니다.");

            systemSetting = new SystemData();

            Debug.Log("SystemData를 생성하였습니다.");

        }
    }

    public void SetVolumeData()
    {
        systemSetting.SFXVolume = AudioManager.Instance.GetSFXVolume();
        systemSetting.BGMVolume = AudioManager.Instance.GetBGMVolume();
    }

    public SystemData GetVolumeData()
    {
        return systemSetting;
    }
}
