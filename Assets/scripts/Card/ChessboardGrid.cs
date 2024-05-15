using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessboardGrid : MonoBehaviour
{
    public int gridSize = 19; // 棋盘大小，例如19x19  
    public float cellSize = 1f; // 每个网格单元的大小  
    private Mesh mesh;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // 创建一个新的Mesh  
        mesh = new Mesh();

        // 顶点数组  
        Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];

        // 填充顶点数组  
        for (int i = 0, y = 0; y <= gridSize; y++)
        {
            for (int x = 0; x <= gridSize; x++)
            {
                vertices[i] = new Vector3(x * cellSize, 0, y * cellSize);
                i++;
            }
        }

        // 三角形索引数组（仅绘制线条）  
        int[] triangles = new int[gridSize * 2 * 3 * (gridSize + 1)]; // 每行有gridSize+1个间隔，每个间隔需要两个三角形（6个索引）  
        int triIndex = 0;

        // 填充三角形索引数组  
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // 垂直线  
                triangles[triIndex++] = y * (gridSize + 1) + x;
                triangles[triIndex++] = y * (gridSize + 1) + x + 1;
                triangles[triIndex++] = (y + 1) * (gridSize + 1) + x;

                // 水平线  
                triangles[triIndex++] = (y + 1) * (gridSize + 1) + x;
                triangles[triIndex++] = y * (gridSize + 1) + x + 1;
                triangles[triIndex++] = (y + 1) * (gridSize + 1) + x + 1;
            }

            // 最后一根垂直线  
            triangles[triIndex++] = y * (gridSize + 1) + gridSize;
            triangles[triIndex++] = y * (gridSize + 1) + gridSize; // 重复顶点以闭合线条  
            triangles[triIndex++] = (y + 1) * (gridSize + 1) + gridSize;
        }

        // 设置Mesh的顶点和三角形  
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // 为了简化，我们可以使用一个默认的无光照着色器和颜色  
        mesh.RecalculateNormals(); // 对于线条网格来说，这步可能是不必要的  

        // 创建一个MeshFilter组件并设置Mesh  
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (!meshFilter)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshFilter.mesh = mesh;

        // 创建一个MeshRenderer组件（如果还没有的话）  
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        // 为MeshRenderer设置一个简单的材质（这里使用默认材质）  
    }
}