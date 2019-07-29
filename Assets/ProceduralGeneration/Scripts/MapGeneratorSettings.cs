using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "map_generator_settings",menuName = "Map Generator Settings")]
public class MapGeneratorSettings : ScriptableObject
{
    public int Width = 10;
    public int Depth = 10;
    public int MaximumRoads = 10;
    public GameObject GroundObject;
    public GameObject BuildingObject;
    public GameObject RoadStraightObject;
    public GameObject RoadTurnObject;
    public GameObject RoadJunctionObject;
}
