using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour
{
    Terrain sand;
    TerrainData originalTerrain;
    float[,] matrix = new float[3,3];
    float maxHeight;
    float hillRadius = 3;
    float resolution;
    int width;
    int height;
    // Start is called before the first frame update
    void Start()
    {
        sand = GetComponent<Terrain>();
        maxHeight = sand.terrainData.size.z;
        width = Mathf.RoundToInt(sand.terrainData.size.x);
        height = Mathf.RoundToInt(sand.terrainData.size.z);
        resolution = sand.terrainData.detailResolution;
        for (int i = 0; i <3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix[i, j] = 1/maxHeight;
            }
        }
        originalTerrain = Resources.Load<TerrainData>("OriginalTerrain");
        sand.terrainData = originalTerrain;
        //CreateHole();
        AddHeight();
    }

    void CreateHole()
    {
        sand.terrainData.SetHeights(1, 1, matrix);
    }
    void AddHeight()
    {
        float[,] original = sand.terrainData.GetHeights(1, 1, matrix.GetLength(0), matrix.GetLength(1));
        sand.terrainData.SetHeights(1, 1, AddMatrices(matrix, original));
    }
    float[,] AddMatrices(float[,] mat1, float[,] mat2)
    {
        float[,] result;
        if (mat1.GetLength(0) == mat2.GetLength(0) && mat1.GetLength(1) == mat2.GetLength(1))
        {
            result = new float[mat1.GetLength(0), mat1.GetLength(0)];
        }
        else
        {
            return null;
        }
        for (int i = 0; i < mat1.GetLength(0); i++)
        {
            for (int j = 0; j < mat1.GetLength(1); j++)
            {
                result[i, j] = mat1[i, j] + mat2[i, j];
            }
        }
        return result;
    }
    private void OnDestroy()
    {
        //ResetTerrain();
    }
    void ResetTerrain()
    {
        
        float[,] zeros = new float[width, height];
        sand.terrainData.SetHeights(width / 2, height/2, zeros);
    }
}
