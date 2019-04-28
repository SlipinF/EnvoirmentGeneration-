using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class TerrainType : UpdatableData
{
    public SpecialElement chunkType;

    public int MinimumAmoutOfObjectsToSpawn;
    public int MaximumAmoutOfObjectsToSpawn;
    public int allowedHeightOfPosition;

    public int amountOfTriesPerType;

    public GameObject[] ElementsToSpawn;
}
