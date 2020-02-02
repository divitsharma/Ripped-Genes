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

    public GameObject first;
    public GameObject second;


    private void Start()
    {
    }

    private void Update()
    {

    }
}
