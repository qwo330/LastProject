using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public abstractEnemy enemy;
    public BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Defines.TAG_PlayerHitBox)
        {
            Player player = other.GetComponentInParent<Player>();  
            player.PlayerWound(Defines.CalculateDamage(enemy.status.Attack, player.status.Defense));
        }   
    }
}