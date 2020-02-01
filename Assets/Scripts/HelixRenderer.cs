using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HelixRenderer : MonoBehaviour
{
    HelixCurve curve;
    public TubeRenderer tubeRendererBlue;
    public TubeRenderer tubeRendererRed;

    void Start()
    {
        curve = GameObject.FindObjectOfType<HelixCurve>();

        RenderHelix();
    }

    void RenderHelix()
    {
        float length = 3 * Mathf.PI;
        float step = 0.05f;
        int npos = (int)(length / step);
        Vector3[] positionsBlue = new Vector3[npos];
        Vector3[] positionsRed = new Vector3[npos];

        for (int i = 0; i < npos; i++)
        {
            float t = step * i;
            positionsBlue[i] = new Vector3(curve.x(t), curve.y(t), curve.z(t));
            positionsRed[i] = new Vector3(curve.x(t), curve.y(t + Mathf.PI), curve.z(t + Mathf.PI));
        }

        tubeRendererBlue.SetPositions(positionsBlue);
        tubeRendererRed.SetPositions(positionsRed);
    }

}
