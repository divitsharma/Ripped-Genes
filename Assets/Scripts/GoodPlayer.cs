using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPlayer : Player
{
    public override void DoAction()
    {
        if (onBP != null && useEnergy())
        {
            //Debug.Log("Doin heals");
            onBP.Repair(50 * Time.deltaTime);
            Speed = 0.1f * MaxSpeed;
        }
    }
}
