using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapGeneratorSettings mapGeneratorSettings;
    public GameObject mapParent;
    public Vector2 origin;

    void Start()
    {
        origin = new Vector2(mapGeneratorSettings.Width / 2f, mapGeneratorSettings.Depth / 2f);
    }

    void Update()
    {
        int mapWidth = 0, mapDepth = 0;

        if (Input.GetKeyUp(KeyCode.G))
        {
            mapWidth = mapGeneratorSettings.Width;
            mapDepth = mapGeneratorSettings.Depth;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            mapWidth = Random.Range(25, 50);
            mapDepth = Random.Range(25, 50);
        }

        if (mapWidth > 0 && mapDepth > 0)
            GenerateMap(mapWidth, mapDepth);
    }

    void GenerateMap(int mapWidth, int mapDepth)
    {
        int result;
        float px, pz;
        float origin_x = mapWidth / 2;
        float origin_z = mapDepth / 2;

        ClearMap();

        for (float z = 0; z < mapDepth; z++)
        {
            for (float x = 0; x < mapWidth; x++)
            {
                px = (z / (z + mapDepth));
                pz = (x / (x + mapWidth));
                result = (int)(Mathf.PerlinNoise(px, pz) * 100.0);
                switch (result % 3)
                {
                    case 0:
                        PutObject(mapGeneratorSettings.BuildingObject, new Vector2(x, z));
                        break;
                    case 1:
                        PutObject(mapGeneratorSettings.GroundObject, new Vector2(x, z));
                        break;
                    default:
                        // PutObject(mapGeneratorSettings.RoadObject, new Vector2(x, z));
                        break;
                }
            }
        }
    }

    void RandomWalkRoad()
    {

    }

    void PutObject(GameObject gameObject, Vector2 position)
    {
        Instantiate(gameObject, new Vector3(position.x, 0, position.y), Quaternion.identity, mapParent.transform);
    }

    void ClearMap()
    {
        int childs = mapParent.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(mapParent.transform.GetChild(i).gameObject);
        }
    }
}
