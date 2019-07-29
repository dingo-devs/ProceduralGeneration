using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ProceduralGeneration.Scripts;

public class MapGenerator : MonoBehaviour
{
    public MapGeneratorSettings mapGeneratorSettings;
    public GameObject mapParent;
    private Vector2 origin;

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
        int[,] map = new int[mapGeneratorSettings.Width, mapGeneratorSettings.Depth];

        ClearMap();

        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, z] = -1;
            }
        }

        RandomWalkRoad(map);

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
    
    void RandomWalkRoad(int[,] map)
    {
        int width = map.GetLength(0);
        int depth = map.GetLength(1);
        if (width < 4 || depth < 4)
            throw new System.Exception("Invalid map size (should be atleast 4x4)");
        
        int x = Random.Range(1, width - 2);
        int y = Random.Range(1, depth - 2);
        RoadDirection prevDirection = RoadDirection.Up;
        RoadDirection currentDirection = RoadDirection.Up;
        

        int maxRoads = Mathf.Min(mapGeneratorSettings.MaximumRoads, map.Length - map.GetLength(0));

        for (int i = 0; i < maxRoads; ++i)
        {
            // Calculate next direction
            prevDirection = currentDirection;

            float probUp, probDown, probLeft, probRight;
            // Check nearby walls
            // Up
            probUp = (y - 1 > 0 && currentDirection != RoadDirection.Down) ? 0.25f : 0.0f;
            // Down
            probDown = (y + 1 < depth - 1 && currentDirection != RoadDirection.Up) ? 0.25f : 0.0f;
            // Left
            probLeft = (x - 1 > 0 && currentDirection != RoadDirection.Right) ? 0.25f : 0.0f;
            // Right
            probRight = (x + 1 < depth - 1 && currentDirection != RoadDirection.Left) ? 0.25f : 0.0f;

            RoadDirectionProbability state = new RoadDirectionProbability(probUp, probLeft, probRight, probDown);
            // Calculate new direction using current state
            currentDirection = state.GetNext();

            // Move in that direction, pay attention to prev direction for turns and shit

        }
    }

    private void IsWall(int x, int z)
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
