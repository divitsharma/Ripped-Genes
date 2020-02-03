using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATP : GeneratedItem
{

    float angle = 0.0f;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        angle += Time.deltaTime;
        transform.Rotate(Vector3.right, angle);
    }

    
}
