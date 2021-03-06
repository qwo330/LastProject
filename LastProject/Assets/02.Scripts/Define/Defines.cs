﻿using UnityEngine;

public static class Defines
{
    public const int InventoryRow = 5;
    public const int InventoryColunm = 6;
    public const int InventorySize = 30;
    public const int TownCount = 2;
    public const int TotalMonsterCount = 3;
    public const int GatherRespawnTime = 5;

    public const int DropItemListCount = 100;
    public const int MaxDropItemCount = 3;
    public const float DropItemProbability = 1;

    public const string TAG_PlayerAttackBox = "PlayerAttackBox";
    public const string TAG_PlayerHitBox = "PlayerHitBox";
    public const string TAG_EnemyAttackBox = "EnemyAttackBox";
    public const string TAG_EnemyHitBox = "EnemyHitBox";
    public const string TAG_Player = "Player";
    public const string TAG_Enemy = "Enemy";

    /// <summary>
    /// 피해량을 계산하는 함수. 나중에 계산식이 바뀌었을 때, 이 함수만 수정하면 됩니다.
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static int CalculateDamage(int atk, int def)
    {
        int totalDamage = atk - def;

        if (totalDamage > 0)
        {
            return totalDamage;
        }
        else
        {
            return 0;
        }
        
    }
}
