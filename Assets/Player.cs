using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected bool onBasePair = false;
    protected BasePairElement onBP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasePairElement>() != null)
        {
            Debug.Log("Colliding");
            onBasePair = true;
            onBP = other.GetComponent<BasePairElement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BasePairElement>() != null)
        {
            Debug.Log("left");
            onBasePair = false;
            onBP = null;
        }
    }

    public virtual void DoAction()
    {

    }
}
