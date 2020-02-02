using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    List<BasePair> basePairs = new List<BasePair>();

    void Start()
    {
        
    }

    public void AddBasePair(BasePair bp)
    {
        basePairs.Add(bp);
    }
}
