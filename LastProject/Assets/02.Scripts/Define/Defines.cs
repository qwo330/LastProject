public static class Defines
{
    public const int InventoryRow = 5;
    public const int InventoryColunm = 6;
    public const int InventorySize = 30;
    public const int TotalMonsterCount = 5;

    public const string TAG_PlayerAttackBox = "PlayerAttackBox";
    public const string TAG_PlayerHitBox = "PlayerHitBox";
    public const string TAG_EnemyAttackBox = "EnemyAttackBox";
    public const string TAG_EnemyHitBox = "EnemyHitBox";

    /// <summary>
    /// 피해량을 계산하는 함수. 나중에 계산식이 바뀌었을 때, 이 함수만 수정하면 됩니다.
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static int CalculateDamage(int atk, int def)
    {
        return atk - def;
    }
}
