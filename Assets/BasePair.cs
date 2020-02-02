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
    public float maxHealth = 100;

    PairType type;
    float health;

    private void Start()
    {
        health = maxHealth;
    }
}
