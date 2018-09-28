using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public BoxCollider Collider;
    public PlayerScr player;

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.OnTargetAttack(other.GetComponent<Enemy>());
    }
}
