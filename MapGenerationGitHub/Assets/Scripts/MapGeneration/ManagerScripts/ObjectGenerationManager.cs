using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerationManager : Singelton<ObjectGenerationManager>
{
    private List<GameObject> generatedObject = new List<GameObject>();
    private int amountOfSpawnedElements = 0;

    public void GenerateChunkObjects(TerrainChunk inChunk){
        int numberOfTries = inChunk.terrainType.amountOfTriesPerType;

        for (int index = 0; index < numberOfTries; index ++){
     
                int RandomVerticeOnChunk = Random.Range(1, inChunk.meshFilter.mesh.vertices.Length);
                int range = Random.Range(0, inChunk.terrainType.ElementsToSpawn.Length);
                generatedObject.Add(SpawnOperation(inChunk, RandomVerticeOnChunk,inChunk.terrainType.ElementsToSpawn[range]));
        }
        GenerateListWithSpawnees(inChunk);
        amountOfSpawnedElements = 0;
        generatedObject.Clear();
    }

    GameObject SpawnOperation(TerrainChunk inChunk,int verticeIndex,GameObject objectToSpawn){  

        Vector3 postionToSpawnOn = RayCastToPosition(CalculatePotentialPosition(inChunk, verticeIndex));
        if(postionToSpawnOn.y <= inChunk.terrainType.allowedHeightOfPosition && postionToSpawnOn != Vector3.zero && amountOfSpawnedElements < inChunk.terrainType.MaximumAmoutOfObjectsToSpawn)
        {
            GameObject newSpawnee = Instantiate(objectToSpawn);
            newSpawnee.transform.position = postionToSpawnOn;
            newSpawnee.transform.Rotate(Vector3.up, Random.Range(0, 180));
            newSpawnee.isStatic = true;
            amountOfSpawnedElements++;
            return newSpawnee;
        }
        else
        {
            return null;
        }
    }

    Vector3 CalculatePotentialPosition(TerrainChunk inChunk, int verticeIndex){
       
        int chunkSize = inChunk.meshSettings.supportedChunkSizes[inChunk.meshSettings.chunkSizeIndex];

        Vector3 positionToRayCastTo = new Vector3(inChunk.meshFilter.mesh.vertices[verticeIndex].x + (chunkSize * inChunk.coord.x),
        inChunk.meshFilter.mesh.vertices[verticeIndex].y, inChunk.meshFilter.mesh.vertices[verticeIndex].z + (chunkSize * inChunk.coord.y));

        return positionToRayCastTo;
    }
    Vector3 RayCastToPosition(Vector3 recivedPosition)
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(recivedPosition + new Vector3(0, 20, 0), Vector3.down, out hit, 50) && hit.transform.tag == "Terrain") {
            return recivedPosition;
        }
        else {
            return Vector3.zero;
        }
    }
    GameObject[] GenerateListWithSpawnees(TerrainChunk inChunk)
    {
        if (generatedObject.Count > 0)
        {
            GameObject treesParentGO = new GameObject("NewObject");
            treesParentGO.transform.SetParent(inChunk.meshObject.transform);

            for (int c = 0; c < generatedObject.Count; c++)
            {
                if (generatedObject[c] != null)
                {
                    generatedObject[c].transform.SetParent(treesParentGO.transform);
                }
            }
        }
        return generatedObject.ToArray();
    }
}
