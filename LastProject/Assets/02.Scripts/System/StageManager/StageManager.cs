using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    ForestVillage, // 숲 마을
    SnowVillage, // 저주받은 눈 마을
    BattleMap,
}

public class StageManager : Singleton<StageManager> {
    public Vector3 playerStartPosition;
    public Stage stage = new ForestVillage();

    SceneState currentStage;
    GameObject player;

    public void ChangeScene(SceneState next)
    {
        currentStage = next;
        stage.SetStage(next);

        Debug.Log("씬 전환 시작");
        SceneManager.LoadScene(currentStage.ToString());
        Debug.Log("씬 전환 종료");
        Debug.Log("플레이어 시작위치 설정");

        //Player 초기 위치 세팅
        //player = ObjectPool.instance.~~
        //player.transform.position = playerStartPosition;
    }
}

public abstract class Stage
{
    abstract public void SetStage(SceneState next);
}

public abstract class TownStage : Stage
{
    // TODO : 마을 배치, NPC 배치
    
    //Townsystem
}

public abstract class FieldStage : Stage
{
    // TODO : 스포너, 배틀시스템, 몬스터 배치

    public GameObject Spawner; // class
    //BattleMediator
}

public class ForestVillage : TownStage
{
    override public void SetStage(SceneState next)
    {
        switch (next)
        {
            case SceneState.BattleMap:
                StageManager.Instance.playerStartPosition = new Vector3(1f, 0f, 48f);
                StageManager.Instance.stage = new BattleMap(); // 미리 만들어도 될 듯
                break;
        }
    }
}

public class SnowVillage : TownStage
{
    override public void SetStage(SceneState next)
    {
        switch (next)
        {
            case SceneState.BattleMap:
                StageManager.Instance.playerStartPosition = new Vector3(-16f, 0f, -47f);
                StageManager.Instance.stage = new BattleMap(); // 미리 만들어도 될 듯
                break;
        }
    }
}

public class BattleMap : FieldStage
{
    override public void SetStage(SceneState next)
    {
        switch (next)
        {
            case SceneState.ForestVillage:
                StageManager.Instance.playerStartPosition = new Vector3(31f, 0f, -13f);
                StageManager.Instance.stage = new ForestVillage(); // 미리 만들어도 될 듯
                break;
            case SceneState.SnowVillage:
                StageManager.Instance.playerStartPosition = new Vector3(31f, 0f, -13f);
                StageManager.Instance.stage = new SnowVillage(); // 미리 만들어도 될 듯
                break;
        }
    }
}
/*
 저주받은 눈 마을->필드 : -16, 0, -47
 숲 마을->필드 : 11, 0, 48
 필드 -> 마을 : 31, 0, -13
     */


/* 
 * 비동기 loadscene 참고 링크
 http://wergia.tistory.com/59
 https://ssscool.tistory.com/309     
*/
