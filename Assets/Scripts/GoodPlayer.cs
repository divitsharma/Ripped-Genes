using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPlayer : Player
{
    public override void DoAction()
    {
        if (onBP != null)
        {
            //Debug.Log("Doin heals");
            onBP.Repair(50 * Time.deltaTime);
        }
    }
}
