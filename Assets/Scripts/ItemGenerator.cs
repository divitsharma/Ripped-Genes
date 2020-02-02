using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{   

    [SerializeField]
    GameObject[] prefabs;
    public float freq;
    public float lowXRange;
    public float highXRange;

    public float lowYRange;
    public float highYRange;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine ()
    {
        WaitForSeconds waitTime = new WaitForSeconds(freq);
        int i = 0;
        int size = prefabs.Length;
        GameObject prefab;
        Vector3 spawnPos = this.transform.position;
        Transform tr = this.transform;
        
        while (true) {
            i++;
            i  = i % size;
            prefab = prefabs[i]; 
            float randomX = Random.Range(lowXRange, highXRange);
            float randomY = Random.Range(lowYRange, highYRange);
            spawnPos.x = randomX;
            spawnPos.y = randomY;
            Instantiate (prefab, spawnPos, Quaternion.identity, tr);
            yield return waitTime;
        }
    }
}
