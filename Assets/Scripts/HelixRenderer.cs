using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class HelixRenderer : MonoBehaviour
{
    // Methematical equation of the helix.
    HelixCurve curve;
    // Meshes of the two strands.
    public TubeRenderer tubeRendererBlue;
    public TubeRenderer tubeRendererRed;

    public GameObject pairPrefab;
    public Quaternion rotation;
    public Vector3 position;

    public int lengthInPI = 5;

    GameState gameState;


    void Start()
    {
        curve = FindObjectOfType<HelixCurve>();
        gameState = FindObjectOfType<GameState>();

        RenderHelix();

        // Apply the parent's rotation and stuff to the children.
        transform.localRotation = rotation;
        transform.localPosition = position;
    }

    void RenderHelix()
    {
        float length = lengthInPI * Mathf.PI;
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

        // Set tube colliders.
        if (Application.isPlaying)
        {
            tubeRendererBlue.gameObject.GetComponent<MeshCollider>().sharedMesh =
                tubeRendererBlue.gameObject.GetComponent<MeshFilter>().mesh;
            tubeRendererRed.gameObject.GetComponent<MeshCollider>().sharedMesh =
                tubeRendererRed.gameObject.GetComponent<MeshFilter>().mesh;
        }

        // Make the base pairs. Each pair has the same x value.
        float pairStep = 0.75f;
        int npairs = (int)(length / pairStep);
        for (float x = 0; x < length; x += pairStep)
        {
            Vector3[] positions = { curve.Ft(x), curve.Ft(x, Mathf.PI) };
            Vector3 direction = positions[1] - positions[0];
            GameObject tb = Instantiate(pairPrefab, positions[1],
                Quaternion.LookRotation(direction, Vector3.up), transform);
            tb.transform.localScale = new Vector3(tb.transform.localScale.x, tb.transform.localScale.y, direction.magnitude);
            // Add to game state.
            BasePair basePair = tb.GetComponent<BasePair>();
            gameState.AddBasePair(basePair);
        }
    }

}
