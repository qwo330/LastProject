using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherspawner : MonoBehaviour
{
    public Generator generator;

    public void Init()
    {
        generator.GetComponentInParent<Generator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Defines.TAG_Player)
        {
            generator.GatherPlayer(this);
            gameObject.SetActive(false);
        }
    }
}
