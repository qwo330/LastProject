using UnityEngine;

public struct CharacterStatus
{
    public int Attack;
    public int Defense;
    public float cHealth;
    public float MaxHealth;
    public int Level;
    public float Exp;
    public float MaxExp;
    public int Gold;

    public CharacterStatus(int atk, int def, int hp)
    {
        Attack = atk;
        Defense = def;
        MaxHealth = hp;
        cHealth = MaxHealth;
        Level = 1;
        Exp = 0;
        MaxExp = 100 + Level * 200;
        Gold = 0;
    }

    public CharacterStatus(int atk, int def, int hp, int Lv)
    {
        Attack = atk;
        Defense = def;
        MaxHealth = hp;
        cHealth = MaxHealth;
        Level = Lv;
        Exp = 0;
        MaxExp = 100 + Level * 200;
        Gold = 0;
    }
}

public static class PlayerAniTrigger
{
    public const string ATTACK = "IsAttack";
    public const string ACTION = "SetAction";
    public const string WOUNDED = "IsWounded";
    public const string DEATH = "IsDeath";
    public const string ISRUNNING = "IsRunnig";
    public const string ISINHOME = "InHome";
    public const string ISIDLE = "IsIdle";
}

/// <summary>
/// 마을 안과 밖일때 isInHome 변수 꼭 조정해줘야함
/// </summary>
public class Player : MonoBehaviour
{
    public bool isInHome;
    public CharacterStatus status;

    PlayerState currentState;
    PlayerState previousState;
    PlayerState[] states;
    Rigidbody rigidbodyComponent;
    Animator animatorComponent;
    PlayerAttackBox attackBoxCollider;

    float VerticalAxis;
    float HorizontalAxis;
    bool isMouseClicked;

    [SerializeField, Range(0, 20)]
    public float MovingSpeed;
    float currentSpeed;

    const int IDLE = 0;
    const int MOVE = 1;
    const int ATTACK = 2;
    const int WOUNDED = 3;
    const int DEATH = 4;
    const int STATE_COUNT = 5;

    public Player Init(int atk, int def, int hp)
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animatorComponent = GetComponent<Animator>();
        attackBoxCollider = GetComponentInChildren<PlayerAttackBox>();
        attackBoxCollider.player = this;
        attackBoxCollider.Collider.enabled = false;

        isInHome = false;
        status = new CharacterStatus(atk, def, hp);

        currentSpeed = MovingSpeed;
        GetExpAndGold(0,0);

        states = new PlayerState[STATE_COUNT];
        states[IDLE] = new PlayerIdle(status, null, null, animatorComponent, null, isInHome, 0);
        states[MOVE] = new PlayerMove(status, transform, rigidbodyComponent, animatorComponent, null, isInHome, currentSpeed);
        states[ATTACK] = new PlayerAttack(status, null, rigidbodyComponent, animatorComponent, attackBoxCollider, false, 0);
        states[WOUNDED] = new PlayerWound(status, null, null, animatorComponent, null, false, currentSpeed);
        states[DEATH] = new PlayerDeath(status, null, null, animatorComponent, null, false, currentSpeed);

        previousState = null;
        currentState = states[IDLE];

        return this;
    }

    private void Update()
    {
        VerticalAxis = InputManager.Instance.VerticalAxis;
        HorizontalAxis = InputManager.Instance.HorizontalAxis;
        isMouseClicked = InputManager.Instance.IsMouseClicked;

        currentSpeed = MovingSpeed;

        previousState = currentState;
        SetCurrentState();
        ChangeState();
    }

    void SetCurrentState()
    {
        if (currentState == states[WOUNDED] || currentState == states[DEATH])
            return;

        if (currentState == states[ATTACK])
            return;

        if (isMouseClicked && !isInHome)
        {
            currentState = states[ATTACK];
        }
        else if ((VerticalAxis != 0 || HorizontalAxis != 0))
        {
            currentState = states[MOVE];
            currentState.currentSpeed = currentSpeed;
        }
        else
        {
            currentState = states[IDLE];
            currentState.isInHome = isInHome;
        }
    }

    void ChangeState()
    {
        if (previousState == null || currentState == null)
            return;

        if (previousState == currentState)
        {
            currentState.Update();
        }
        else
        {
            previousState.Exit();
            currentState.Enter();
        }
    }

    void OnAttackExit()
    {
        SetPlayerState(states[IDLE]);
    }

    void OnWoundExit()
    {
        SetPlayerState(states[IDLE]);
    }

    public void PlayerWound(int damege)
    {
        if (currentState == states[DEATH])
            return;

        status.cHealth -= damege;
        UIPresenter.Instance.DrawPlayerUI(status);

        if (status.cHealth < 0)
        {
            SetPlayerState(states[DEATH]);
        }

        else
        {
            SetPlayerState(states[WOUNDED]);
        }
    }

    public void GetExpAndGold(int exp, int gold)
    {
        status.Exp += exp;
        status.Gold += gold;
        if (status.Exp > status.MaxExp)
        {
            status.Level++;
            status.Exp = 0;
        }
        status.MaxExp = 50 + status.Level * 100;
        UIPresenter.Instance.DrawPlayerUI(status);
    }

    //플레이어를 풀에서 꺼낼 때 Idle로 초기화 시킴
    public void SetPlayerState()
    {
        SetPlayerState(states[IDLE]);
    }

    void SetPlayerState(PlayerState state)
    {
        previousState = currentState;
        currentState = state;
        previousState.Exit();
        currentState.Enter();
    }
}