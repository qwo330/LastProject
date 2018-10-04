using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=84613
public enum SceneState
{
    ForestVillage, // 숲 마을
    SnowVillage, // 저주받은 눈 마을
    BattleMap,
}

public class StageManager : Singleton<StageManager> {
    public Vector3 playerStartPosition;
    public Stage stage;
    [SerializeField]
    SceneState currentStage;
    [SerializeField]
    GameObject player;

    public void Init()
    {
        player = GameObject.FindWithTag("PlayerHitBox");
        currentStage = SceneState.BattleMap;
        stage = new BattleMap();
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
        //SceneManager.LoadScene(currentStage.ToString());
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentStage.ToString());
        //asyncOperation.allowSceneActivation = false;
        //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(currentStage.ToString()));
        //asyncOperation.allowSceneActivation = true;
        StartCoroutine(LoadAsyncScene());
        Debug.Log("씬 전환 종료");
        Debug.Log("플레이어 시작위치 설정");

        //Player 초기 위치 세팅
        //player = ObjectPool.instance.~~
        
    }

    IEnumerator LoadAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentStage.ToString(), LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(currentStage.ToString()));
        player.transform.position = playerStartPosition;
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
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
                StageManager.Instance.SetPlayerPosition(new Vector3(32f, 1.5f, -13f));
                StageManager.Instance.stage = new ForestVillage(); // 미리 만들어도 될 듯
                break;
            case SceneState.SnowVillage:
                StageManager.Instance.SetPlayerPosition(new Vector3(32f, 1.5f, -13f));
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
