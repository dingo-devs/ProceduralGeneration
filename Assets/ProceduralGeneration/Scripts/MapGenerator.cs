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
                float x = (depth / (NoiseMap.Depth + 1)) + NoiseMap.Seed / long.MaxValue;
                float y = (width / (NoiseMap.Width + 1)) + NoiseMap.Seed / long.MaxValue;
                noise = Mathf.PerlinNoise(x ,y);
                if (noise < 0.3f)
                {
                    Instantiate(NoiseMap.TileObject, new Vector3(width - NoiseMap.Width / 2, 0.4f, depth - NoiseMap.Depth / 2), Quaternion.identity, this.transform);
                }
                else
                {
                    Instantiate(NoiseMap.GroundObject, new Vector3(width - NoiseMap.Width / 2, 0, depth - NoiseMap.Depth / 2), Quaternion.identity, this.transform);
                }
            }
        }
    }
}
