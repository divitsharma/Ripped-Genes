using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedItem : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    public float deleteTime;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * speed);
        Debug.Log("start");
    }

    // Update is called once per frame
    protected void Update()
    {
        DestroyObjectDelayed();
    }

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, deleteTime);
    }
}
