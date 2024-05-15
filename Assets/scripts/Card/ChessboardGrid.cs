using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessboardGrid : MonoBehaviour
{
    public int gridSize = 19; // ���̴�С������19x19  
    public float cellSize = 1f; // ÿ������Ԫ�Ĵ�С  
    private Mesh mesh;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // ����һ���µ�Mesh  
        mesh = new Mesh();

        // ��������  
        Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];

        // ��䶥������  
        for (int i = 0, y = 0; y <= gridSize; y++)
        {
            for (int x = 0; x <= gridSize; x++)
            {
                vertices[i] = new Vector3(x * cellSize, 0, y * cellSize);
                i++;
            }
        }

        // �������������飨������������  
        int[] triangles = new int[gridSize * 2 * 3 * (gridSize + 1)]; // ÿ����gridSize+1�������ÿ�������Ҫ���������Σ�6��������  
        int triIndex = 0;

        // �����������������  
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // ��ֱ��  
                triangles[triIndex++] = y * (gridSize + 1) + x;
                triangles[triIndex++] = y * (gridSize + 1) + x + 1;
                triangles[triIndex++] = (y + 1) * (gridSize + 1) + x;

                // ˮƽ��  
                triangles[triIndex++] = (y + 1) * (gridSize + 1) + x;
                triangles[triIndex++] = y * (gridSize + 1) + x + 1;
                triangles[triIndex++] = (y + 1) * (gridSize + 1) + x + 1;
            }

            // ���һ����ֱ��  
            triangles[triIndex++] = y * (gridSize + 1) + gridSize;
            triangles[triIndex++] = y * (gridSize + 1) + gridSize; // �ظ������Ապ�����  
            triangles[triIndex++] = (y + 1) * (gridSize + 1) + gridSize;
        }

        // ����Mesh�Ķ����������  
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Ϊ�˼򻯣����ǿ���ʹ��һ��Ĭ�ϵ��޹�����ɫ������ɫ  
        mesh.RecalculateNormals(); // ��������������˵���ⲽ�����ǲ���Ҫ��  

        // ����һ��MeshFilter���������Mesh  
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (!meshFilter)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshFilter.mesh = mesh;

        // ����һ��MeshRenderer����������û�еĻ���  
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        // ΪMeshRenderer����һ���򵥵Ĳ��ʣ�����ʹ��Ĭ�ϲ��ʣ�  
    }
}