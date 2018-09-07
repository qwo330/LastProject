using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{

    public int HP = 100;
    public int MaxHP = 100;

    public int Power = 10;

    public GameObject lastAttackTarget = null;

     
    public bool Attacking = false;
    public bool Hit = false;
    public bool Dead = false;
}
