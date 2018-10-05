using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=84613
public enum SceneState
{
    ForestVillage, // 숲 마을
    SnowVillage, // 저주받은 눈 마을
    BattleMap,
    Field1,
}

public class StageManager : Singleton<StageManager> {
    public Vector3 playerStartPosition;
    public Stage stage;

    [SerializeField]
    SceneState currentStage;

    [SerializeField]
    Player player;

    public void Init()
    {
        currentStage = SceneState.Field1;
        stage = new BattleMap();
        player = ObjectPool.Instance.PopPlayer(playerStartPosition);
    }

    public void SetPlayerPosition(Vector3 startPosition)
    {
        playerStartPosition = startPosition;
    }

    public void ChangeScene(SceneState next)
    {
        currentStage = next;
        stage.SetStage(next);

        Debug.Log("씬 전환 시작");

        SceneManager.LoadScene(currentStage.ToString());
        //stage.LoadScene(next);

        Debug.Log("씬 전환 종료");
        Debug.Log("플레이어 시작위치 설정 :  " + playerStartPosition);

        player = ObjectPool.Instance.PopPlayer(playerStartPosition);
        //stage.Test();
    }
}

public abstract class Stage
{
    abstract public void SetStage(SceneState next);
    abstract public void LoadScene(SceneState next);
    abstract public void Test();
}

public abstract class TownStage : Stage
{
    // TODO : 마을 배치, NPC 배치
    public GameObject[] Level1s;
    public GameObject[] Level2s;

    override public void LoadScene(SceneState next)
    {
        SceneManager.LoadScene(next.ToString());
    }

    public override void Test()
    {
        Level1s = GameObject.FindGameObjectsWithTag("TownLevel1");
        Level2s = GameObject.FindGameObjectsWithTag("TownLevel2");

        for (int i = 0; i < Level1s.Length; i++)
        {
            Debug.Log(Level1s[i].name);
        }
    }


    //Townsystem
}

public abstract class FieldStage : Stage
{
    // TODO : 스포너, 배틀시스템, 몬스터 배치
    override public void LoadScene(SceneState next)
    {
        SceneManager.LoadScene(next.ToString());
    }

    public override void Test()
    {
        Debug.Log("배틀맵");
    }

    public GameObject Spawner; // class
    //BattleMediator
}

public class ForestVillage : TownStage
{
    override public void SetStage(SceneState next)
    {
        switch (next)
        {
            case SceneState.Field1:
                StageManager.Instance.playerStartPosition = new Vector3(12f, 2f, 47f);
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
            case SceneState.Field1:
                StageManager.Instance.playerStartPosition = new Vector3(-16f, 2f, -47f);
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
                StageManager.Instance.SetPlayerPosition(new Vector3(33f, 1.5f, -13f));
                StageManager.Instance.stage = new ForestVillage(); // 미리 만들어도 될 듯
                break;
            case SceneState.SnowVillage:
                StageManager.Instance.SetPlayerPosition(new Vector3(33f, 1.5f, -13f));
                StageManager.Instance.stage = new SnowVillage(); // 미리 만들어도 될 듯
                break;
        }
    }
}


/* 
 * 비동기 loadscene 참고 링크
 http://wergia.tistory.com/59
 https://ssscool.tistory.com/309     
*/
