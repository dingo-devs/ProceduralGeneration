using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "noise_map",menuName = "Noise Map")]
public class NoiseMap : ScriptableObject
{
    public int Width = 10;
    public int Depth = 10;
    public float Scale = 1;
    public GameObject GroundObject;
    public GameObject TileObject;
}
