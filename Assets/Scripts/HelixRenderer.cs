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
    public GameObject pairPrefab2;

    public GameObject barrierProtein;
    public float minClamp;
    public float maxClamp;

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

            float r = Random.Range(0f, 1f);
            GameObject pf;
            if (r > 0.5f)
            {
                pf = pairPrefab;
            }
            else
            {
                pf = pairPrefab2;
            }
            GameObject tb = Instantiate(pf, positions[1],
                Quaternion.LookRotation(direction, Vector3.up), transform);
            tb.transform.localScale = new Vector3(tb.transform.localScale.x, tb.transform.localScale.y, direction.magnitude);
            // Add to game state.
            BasePair basePair = tb.GetComponent<BasePair>();
            if (gameState != null)
            gameState.AddBasePair(basePair);
        }

        // Make barrier proteins
        Vector3[] positions2 = { curve.Ft(minClamp * Mathf.PI), curve.Ft(minClamp * Mathf.PI, Mathf.PI), curve.Ft(maxClamp * Mathf.PI), curve.Ft(maxClamp * Mathf.PI, Mathf.PI)};
        Vector3 direction1 = positions2[1] - positions2[0];
        Vector3 direction2 = positions2[3] - positions2[2];
        GameObject bp1 = Instantiate(barrierProtein, positions2[0], Quaternion.LookRotation(direction1, Vector3.up), transform);
        GameObject bp2 = Instantiate(barrierProtein, positions2[1], Quaternion.LookRotation(-1 * direction1, Vector3.up), transform);
        GameObject bp3 = Instantiate(barrierProtein, positions2[2], Quaternion.LookRotation(direction2, Vector3.up), transform);
        GameObject bp4 = Instantiate(barrierProtein, positions2[3], Quaternion.LookRotation(-1 * direction2, Vector3.up), transform);


    }

}
