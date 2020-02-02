using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    public override void DoAction()
    {
        if (onBP != null)
        {
            //Debug.Log("Doin dmg");
            onBP.TakeDamage(50 * Time.deltaTime);
            useEnergy(10 * Time.deltaTime);
            Speed = 0.1f * MaxSpeed;
        }
    }
}
