using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public PlayerScr player;

    public void PlayerWound(int damage)
    {
        player.PlayerWound(damage);
    }
}
