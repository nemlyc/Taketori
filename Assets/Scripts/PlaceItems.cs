using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItems : MonoBehaviour
{
    public GameObject bamboo;
    [SerializeField] Terrain terrain;
    public int itemcount = 100;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        
        for(int i = 0; i < itemcount; i++)
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float height = terrain.terrainData.GetInterpolatedHeight(x / terrain.terrainData.size.x, z / terrain.terrainData.size.z);
            Vector3 spawnposi = new Vector3(x, height, z);
            Instantiate(bamboo, spawnposi, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
