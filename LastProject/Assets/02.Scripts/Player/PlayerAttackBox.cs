using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public BoxCollider Collider;
    public Player player;

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Defines.TAG_EnemyHitBox)
        {
            abstractEnemy enemy = other.GetComponentInParent<abstractEnemy>();
            enemy.PlayerWound(Defines.CalculateDamage(player.status.Attack, enemy.status.Defense));
        }
    }
}