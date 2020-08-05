using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public float xSize = 100f;
    public float zSize = 100f;
    public float ySize = 1f;
    public int xResolution = 100;
    public int zResolution = 100;
    public float terrainDepth = 30f;
    public float scale = 2f;
    public float xOffset = 100f;
    public float zOffset = 100f;

    public Gradient gradient;

    float minTerrainHeight;
    float maxTerrainHeight;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
        UpdateSize();
    }

    private void UpdateSize()
    {
        transform.localScale = new Vector3(xSize/xResolution, ySize, zSize/zResolution);
    }

    void CreateShape()
    {
        vertices = new Vector3[(xResolution + 1)*(zResolution + 1)];

        for (int i = 0, z = 0; z <= zResolution; z++)
        {
            for (int x = 0; x <= xResolution; x++)
            {
                float y = CalculateHeight(x, z);
                vertices[i] = new Vector3(x, y, z);

                if(y>maxTerrainHeight)
                {
                    maxTerrainHeight = y;
                }
                if(y<minTerrainHeight)
                {
                    minTerrainHeight = y;
                }
                i++;
            }
        }

        triangles = new int[xResolution*zResolution*6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zResolution; z++)
        {
            for (int x = 0; x < xResolution; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xResolution + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xResolution + 1;
                triangles[tris + 5] = vert + xResolution + 2;

                vert++;
                tris += 6; 
            }
            vert++;
        }

        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zResolution; z++)
        {
            for (int x = 0; x <= xResolution; x++)
            {
                float height  = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height); 
                i++;
            }
        }

    }

    private float CalculateHeight(int x, int z)
    {
        float xCoord = (float) x/xResolution * scale + xOffset;
        float yCoord = (float) z/zResolution * scale + zOffset;

        return Mathf.PerlinNoise(xCoord, yCoord)*terrainDepth;
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos() 
    {
        if(vertices == null) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

}
