using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text atp;

    public float MaxSpeed;
    [HideInInspector] public float Speed;
    protected bool onBasePair = false;
    protected BasePairElement onBP;

    public float energyStore;
    public float energyReqd;
    public float ATPEnergy;

    public GameObject ATPDie;

    private void Start()
    {
        Speed = MaxSpeed;
    }
    
    private void Update() {
        atp.text = "atp: " + energyStore;
    }

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
        }  else if (other.GetComponent<ATP>() != null) {
            getEnergy();
            other.gameObject.SetActive(false);
            Instantiate(ATPDie, transform.position, transform.rotation);
        }
    }

    public virtual void DoAction()
    {

    }

    public void getEnergy() {
        energyStore += ATPEnergy;
    }

    public bool useEnergy() {
        if (energyStore >= energyReqd) {
            energyStore -= energyReqd;
            return true;
        } else return false;
    }
}
