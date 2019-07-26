using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public NoiseMap NoiseMap;
    public GameObject MapParent;

    void Update()
    {
        int mapWidth = 0, mapDepth = 0;
        float scale = 0.0f;

        if (Input.GetKeyUp(KeyCode.G))
        {
            mapWidth = NoiseMap.Width;
            mapDepth = NoiseMap.Depth;
            scale = NoiseMap.Scale;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            mapWidth = Random.Range(25, 50);
            mapDepth = Random.Range(25, 50);
            scale = Random.Range(0.5f, 4.0f);
        }

        if (mapWidth > 0 && mapDepth > 0)
            GenerateMap(mapWidth, mapDepth, scale);
    }

    void GenerateMap(int mapWidth, int mapDepth, float scale)
    {
        float result, px, pz;
        float origin_x = mapWidth / 2;
        float origin_z = mapDepth / 2;

        ClearMap();

        Debug.Log($"[{mapWidth} X {mapDepth}] * {scale}");

        for (float z = 0; z < mapDepth; z++)
        {
            for (float x = 0; x < mapWidth; x++)
            {
                px = (z / (z + mapDepth * scale));
                pz = (x / (x + mapWidth * scale));
                result = Mathf.PerlinNoise(px, pz);
                if ((int)(result * 100.0 * scale) % 2 == 0)
                {
                    Instantiate(NoiseMap.TileObject, new Vector3(x - origin_x, 0.5f, z - origin_z), Quaternion.identity, MapParent.transform);
                }
                else
                {
                    Instantiate(NoiseMap.GroundObject, new Vector3(x - origin_x, 0, z - origin_z), Quaternion.identity, MapParent.transform);
                }
            }
        }
    }

    void ClearMap()
    {
        int childs = MapParent.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(MapParent.transform.GetChild(i).gameObject);
        }
    }
}
