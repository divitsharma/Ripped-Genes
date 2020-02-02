using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCamera : MonoBehaviour
{

    [SerializeField] 
    Transform[] targets;

    [SerializeField] 
    float boundingBoxPadding = 2f;

    [SerializeField]
    float minimumOrthographicSize = 8f;

    [SerializeField]
    float zoomSpeed = 20f;

    Camera camera2;

    float minPosition; // left border
    float maxPosition; //  right border

    void Awake () 
    {
        camera2 = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Rect boundingBox = CalculateTargetsBoundingBox();
        transform.position = CalculateCameraPosition(boundingBox);
    }

    /// <summary>
    /// Calculates a bounding box that contains all the targets.
    /// </summary>
    /// <returns>A Rect containing all the targets.</returns>
    Rect CalculateTargetsBoundingBox()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (Transform target in targets) {
            Vector3 position = target.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }

        return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
    }

    /// <summary>
    /// Calculates a camera position given the a bounding box containing all the targets.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A Vector3 in the center of the bounding box.</returns>
    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;
        float calculatedZ = calculateZ();

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, calculatedZ);
    }

    public float calculateZ() {
        float left = viewportLeft(camera2, targets[0]);
        float right = viewportRight(camera2, targets[0]);
        foreach (Transform target in targets) {
            Vector3 position = target.position;
            if (position.x < left || position.x > right) {

            } 
        }
        return 0f;
    }

     public float viewportLeft(Camera c, Transform t) {
        float yFromCamera = t.transform.position.z - c.transform.position.z;

        return c.ViewportToWorldPoint(new Vector3(0f, 1f, yFromCamera)).x;
    }

      public float viewportRight(Camera c, Transform t) {
        float yFromCamera = t.transform.position.z - c.transform.position.z;

        return c.ViewportToWorldPoint(new Vector3(1f, 1f, yFromCamera)).x;
    }

    

    public float NominalScreenHeightAt(Camera c, Transform t) {
        float yFromCamera = t.transform.position.z - c.transform.position.z;

        return
            c.ViewportToWorldPoint(new Vector3(0f, 1f, yFromCamera)).y
            - c.ViewportToWorldPoint(new Vector3(0f, 0f, yFromCamera)).y;
    }
}

