using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HelixRenderer : MonoBehaviour
{
    HelixCurve curve;
    public TubeRenderer tubeRendererBlue;
    public TubeRenderer tubeRendererRed;

    public Material pairMaterial;

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
        
        // Make the strands.
        for (int i = 0; i < npos; i++)
        {
            float t = step * i;
            positionsBlue[i] = curve.Ft(t);
            positionsRed[i] = curve.Ft(t, Mathf.PI);
        }

        tubeRendererBlue.SetPositions(positionsBlue);
        tubeRendererRed.SetPositions(positionsRed);

        // Make the base pairs. Each pair has the same x value.
        float pairStep = 0.5f;
        int npairs = (int)(length / pairStep);
        for (float x = 0; x < length; x += pairStep)
        {
            Vector3[] positions = { curve.Ft(x), curve.Ft(x, Mathf.PI) };
            TubeRenderer tb = gameObject.AddComponent<TubeRenderer>();
            tb.SetPositions(positions);
            tb.material = pairMaterial;
        }
    }

}
