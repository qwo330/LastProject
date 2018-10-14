using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherspawner : MonoBehaviour
{
    [SerializeField] ItemCodes item;
    public Generator generator;
    public int Index;

    public void Init(int index)
    {
        generator = GetComponentInParent<Generator>();
        this.Index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Defines.TAG_Player)
        {
            generator.GatherItem(Index, item);
            gameObject.SetActive(false);
        }
    }
}
