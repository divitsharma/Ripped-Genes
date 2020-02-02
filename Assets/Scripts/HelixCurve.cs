using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixCurve : MonoBehaviour
{
    public float zr;
    public float yr;
    public float c;

    public Vector3 Ft(float t, float offset = 0f)
    {
        return new Vector3(x(t), y(t + offset), z(t + offset));
    }

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
        float twopi = 5 * Mathf.PI;

        float zoffset = zr;

        for (float t = 0; t < twopi; t += 0.02f)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Ft(t), 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Ft(t, Mathf.PI), 0.1f);
        }
    }
}
