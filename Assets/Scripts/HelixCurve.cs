using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixCurve : MonoBehaviour
{
    public float zr;
    public float yr;
    public float c;

    public float x(float t)
    {
        return c * t;
    }

    public float y(float t)
    {
        return yr * Mathf.Cos(t);
    }

    public float z(float t)
    {
        return zr * Mathf.Sin(t);
    }

    private void OnDrawGizmosSelected()
    {
        float twopi = 3 * Mathf.PI;

        float zoffset = zr;

        for (float t = 0; t < twopi; t += 0.02f)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(x(t), y(t), z(t)), 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(x(t), y(t + Mathf.PI), z(t + Mathf.PI)), 0.1f);
        }
    }
}
