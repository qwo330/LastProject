using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    Title = 1,

    Village1 = 10,
    SnowVillage = 11, // 저주받은 눈 마을

    Field1 = 100, 
}

public class StageManager : Singleton<StageManager>{
    public Vector3 playerStartPosition;
    public Stage stage;

    [SerializeField]
    SceneState currentStage;

    public Player player { get; private set; }

    float fadeTime = 1f, waitTime = 1f;
    Color fadeColor;
    SpriteRenderer fadeObject;
    WaitForEndOfFrame frameDelay = new WaitForEndOfFrame();

    public void Init()
    {
        currentStage = SceneState.Title;
        stage = new ForestVillage();

        player = ObjectPool.Instance.PopPlayer(playerStartPosition);        
        fadeObject = player.GetComponentInChildren<SpriteRenderer>();
    }

    public void SetPlayerPosition(Vector3 startPosition)
    {
        playerStartPosition = startPosition;
    }

    public void ChangeScene(SceneState next)
    {
        if (currentStage != SceneState.Title)
            ObjectPool.Instance.allPushEnt();

        currentStage = next;
        stage.SetStage(next);
                                                   // 마을 : 필드
        player.isInHome = ((int)currentStage < 100) ? true : false;

        StartCoroutine(FadeOut()); // 로딩
    }


    IEnumerator FadeOut() // 시작 // 점점 검게
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            yield return frameDelay;
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeTime);
            fadeObject.color = fadeColor;
        }
        SceneManager.LoadScene(currentStage.ToString()); // 씬 전환

        player = ObjectPool.Instance.PopPlayer(playerStartPosition);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() // 끝 // 점점 투명하게
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            yield return frameDelay;
            elapsedTime += Time.deltaTime;
            fadeColor.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            fadeObject.color = fadeColor;
        }
    }
}

public abstract class Stage
{
    abstract public void SetStage(SceneState next);
}

public class ForestVillage : Stage
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

public class SnowVillage : Stage
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

public class BattleMap : Stage
{
    override public void SetStage(SceneState next)
    {
        switch (next)
        {
            case SceneState.Village1:
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

//public abstract class TownStage : Stage
//{
//    // TODO : 마을 배치, NPC 배치
//    public GameObject[] Level1s;
//    public GameObject[] Level2s;

//    override public void LoadScene(SceneState next)
//    {
//        SceneManager.LoadScene(next.ToString());
//    }

//    public override void Test()
//    {
//        Level1s = GameObject.FindGameObjectsWithTag("TownLevel1");
//        Level2s = GameObject.FindGameObjectsWithTag("TownLevel2");

//        for (int i = 0; i < Level1s.Length; i++)
//        {
//            Debug.Log(Level1s[i].name);
//        }
//    }


//    //Townsystem
//}

//public abstract class FieldStage : Stage
//{
//    // TODO : 스포너, 배틀시스템, 몬스터 배치
//    override public void LoadScene(SceneState next)
//    {
//        SceneManager.LoadScene(next.ToString());
//    }

//    public override void Test()
//    {
//        Debug.Log("배틀맵");
//    }

//    public GameObject Spawner; // class
//    //BattleMediator
//}