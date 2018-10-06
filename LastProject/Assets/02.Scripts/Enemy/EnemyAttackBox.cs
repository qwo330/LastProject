using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public abstractEnemy enemy;
    public BoxCollider colliderComponent;

    private void Start()
    {
        colliderComponent = GetComponent<BoxCollider>();
        colliderComponent.enabled = false;
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