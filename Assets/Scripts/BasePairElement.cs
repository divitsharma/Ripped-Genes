using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePairElement : MonoBehaviour
{
    public float maxHealth = 100;
    public bool isBeingAttacked = false;

    PairType type;
    private float health;
    public float Health { get => health; }

    Material materialInstance;


    private void Start()
    {
        health = maxHealth;
        materialInstance = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (health < maxHealth)
        {
            // Start pulsing.
            materialInstance.SetFloat("Vector1_5AF52C91", 0f);
        }
        else
        {
            // Stop pulsing.
            materialInstance.SetFloat("Vector1_5AF52C91", 1f);
        }

        materialInstance.SetFloat("Vector1_27734CA3", 1f - (health / maxHealth));

        if  (health <= 0)
        {
            //gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
            health -= damage;
    }

    public void Repair(float damage)
    {
        if (health < maxHealth)
            health += damage;
    }
}
