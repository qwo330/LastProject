using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    CharacterStatus status;
    Vector3 prePosition;
    bool isDead = false;
    bool attacked = false;

    public bool IsAttacked()
    {
        return attacked;
    }

    void StartAttackHit()
    {
        Debug.Log("StartAttackHit");
    }

    void EndHit()
    {
        Debug.Log("EndHit");
    }

    void EndAttack()
    {
        attacked = true;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
        prePosition = transform.position;
    }

    void Update()
    {
        Vector3 deltaPosition = transform.position - prePosition;
        animator.SetFloat("Speed", (deltaPosition.magnitude / 2)/ Time.deltaTime);

        if (attacked && status.Attacking == false)
        {
            attacked = false;
        }
        animator.SetBool("Attack1", (attacked == false && status.Attacking == true));
        animator.SetBool("Hit", (attacked = false || status.Hit == true));

         
        if (status.Attacking == false && status.Dead==true)
        {
            isDead = true;
            animator.SetTrigger("Dead");
        }
        prePosition = transform.position;

    }
}
