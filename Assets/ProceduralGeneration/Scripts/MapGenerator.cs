using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public NoiseMap NoiseMap;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        float noise = 0;
        for (float depth = 0; depth < NoiseMap.Depth; depth++)
        {
            for (float width = 0; width < NoiseMap.Width; width++)
            {
                float x = (depth / NoiseMap.Depth) + NoiseMap.Seed / 10000.0f;
                float y = (width / NoiseMap.Width) + NoiseMap.Seed / 10000.0f;
                noise = Mathf.PerlinNoise(x ,y);
                if (noise < 0.3f)
                {
                    Instantiate(NoiseMap.TileObject, new Vector3(width, 0.4f, depth), Quaternion.identity, this.transform);
                }
                else
                {
                    Instantiate(NoiseMap.GroundObject, new Vector3(width, 0, depth), Quaternion.identity, this.transform);
                }
            }
        }
    }
}
