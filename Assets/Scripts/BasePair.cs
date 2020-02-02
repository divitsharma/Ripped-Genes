using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PairType
{
    AT,
    GC
}

public class BasePair : MonoBehaviour
{
    PairType type;
    float health;

    public BasePairElement first;
    public BasePairElement second;


    private void Start()
    {
    }

    public bool IsBroken()
    {
        return first.Health < first.maxHealth && second.Health < second.maxHealth;
    }
}
