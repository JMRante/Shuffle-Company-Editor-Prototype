using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush
{
    public int topTexIndex;
    public int westTexIndex;
    public int eastTexIndex;
    public int southTexIndex;

    public Brush()
    {
        topTexIndex = 0;
        westTexIndex = 0;
        eastTexIndex = 0;
        southTexIndex = 0;
    }

    public Brush(int texIndex)
    {
        topTexIndex = texIndex;
        westTexIndex = texIndex;
        eastTexIndex = texIndex;
        southTexIndex = texIndex;
    }

    public Brush(int topTexIndex, int bottomTexIndex)
    {
        this.topTexIndex = topTexIndex;
        westTexIndex = bottomTexIndex;
        eastTexIndex = bottomTexIndex;
        southTexIndex = bottomTexIndex;
    }

    public Brush(int topTexIndex, int westTexIndex, int eastTexIndex, int southTexIndex)
    {
        this.topTexIndex = topTexIndex;
        this.westTexIndex = westTexIndex;
        this.eastTexIndex = eastTexIndex;
        this.southTexIndex = southTexIndex;
    }
}

public class Stage : MonoBehaviour
{
    private const int MAX_WIDTH = 200;
    private const int MAX_HEIGHT = 10;
    private const int MAX_DEPTH = 200;

    private const int MIN_WIDTH = 5;
    private const int MIN_HEIGHT = 5;
    private const int MIN_DEPTH = 5;

    private int width = 10;
    public int Width
    {
        get
        {
            return width;
        }
        set
        {
            width = Mathf.Clamp(value, MIN_WIDTH, MAX_WIDTH);
        }
    }

    private int height = 10;
    public int Height
    {
        get
        {
            return height;
        }
        set
        {
            height = Mathf.Clamp(value, MIN_HEIGHT, MAX_HEIGHT);
        }
    }

    private int depth = 10;
    public int Depth
    {
        get
        {
            return depth;
        }
        set
        {
            depth = Mathf.Clamp(value, MIN_DEPTH, MAX_DEPTH);
        }
    }

    private int[] stage = new int[MAX_WIDTH * MAX_HEIGHT * MAX_DEPTH];
    private int[] stageBuffer = new int[MAX_WIDTH * MAX_HEIGHT * MAX_DEPTH];

    private List<Brush> brushList;
    public List<Texture2D> textureList;
    private Texture2DArray textureArray;

	void Start ()
    {
        brushList = new List<Brush>(512);
        brushList.Add(new Brush(1, 0));

        textureList = new List<Texture2D>();

        BuildMesh();
    }

    public void BuildMesh()
    {
        // Check if stage object has a mesh filter and get it
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.Log("Cannot build stage, object has no mesh filter.");
        }
        else
        {
            // Prepare to build the stage mesh
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector3> uvs = new List<Vector3>();
            List<int> triangles = new List<int>();

            // Build stage mesh
            Vector3 vertex;
            Vector3 normal;
            Vector3 uv;
            int triangle = 0;

            // Loop through stage blocks
            for (int i = 0; i < MAX_WIDTH; i++)
            {
                for (int j = 0; j < MAX_HEIGHT; j++)
                {
                    for (int k = 0; k < MAX_DEPTH; k++)
                    {
                        // If the grid space is not empty, build!
                        if (GetBlock(i, j, k) > 0)
                        {
                            Brush brush = brushList[GetBlock(i, j, k) - 1];

                            // Build block top side
                            if (j + 1 >= MAX_HEIGHT || GetBlock(i, j + 1, k) == 0)
                            {
                                // 1st Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normal.x = 0.0f;
                                normal.y = 1.0f;
                                normal.z = 0.0f;
                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 1.0f;
                                uv.z = brush.topTexIndex;
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uv.z = brush.topTexIndex;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 0.0f;
                                uv.z = brush.topTexIndex;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
                                uv.z = brush.topTexIndex;
                                uvs.Add(uv);

                                // 1st Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 1);
                                triangles.Add(triangle + 2);

                                // 2nd Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 3);
                                triangles.Add(triangle + 1);

                                // Increment indices to next set of vertices
                                triangle += 4;
                            }

                            // Build block west side
                            if (i - 1 <= -1 || GetBlock(i - 1, j, k) == 0)
                            {
                                // 1st Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normal.x = -1.0f;
                                normal.y = 0.0f;
                                normal.z = 0.0f;
                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 1.0f;
                                uv.z = brush.westTexIndex;
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uv.z = brush.westTexIndex;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
                                uv.z = brush.westTexIndex;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 0.0f;
                                uv.z = brush.westTexIndex;
                                uvs.Add(uv);

                                // 1st Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 1);
                                triangles.Add(triangle + 2);

                                // 2nd Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 3);
                                triangles.Add(triangle + 1);

                                // Increment indices to next set of vertices
                                triangle += 4;
                            }

                            // Build block east side
                            if (i + 1 >= MAX_WIDTH || GetBlock(i + 1, j, k) == 0)
                            {
                                // 1st Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normal.x = 1.0f;
                                normal.y = 0.0f;
                                normal.z = 0.0f;
                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 1.0f;
                                uv.z = brush.eastTexIndex;
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uv.z = brush.eastTexIndex;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
                                uv.z = brush.eastTexIndex;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 0.0f;
                                uv.z = brush.eastTexIndex;
                                uvs.Add(uv);

                                // 1st Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 1);
                                triangles.Add(triangle + 2);

                                // 2nd Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 3);
                                triangles.Add(triangle + 1);

                                // Increment indices to next set of vertices
                                triangle += 4;
                            }

                            // Build block south side
                            if (k - 1 <= -1 || GetBlock(i, j, k - 1) == 0)
                            {
                                // 1st Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normal.x = 0.0f;
                                normal.y = 0.0f;
                                normal.z = -1.0f;
                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
                                uv.z = brush.southTexIndex;
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 0.0f;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 1.0f;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uvs.Add(uv);

                                // 1st Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 1);
                                triangles.Add(triangle + 2);

                                // 2nd Triangle
                                triangles.Add(triangle);
                                triangles.Add(triangle + 3);
                                triangles.Add(triangle + 1);

                                // Increment indices to next set of vertices
                                triangle += 4;
                            }
                        }
                    }
                }
            }

            // Add built mesh data to MeshFilter component
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.normals = normals.ToArray();
            mesh.SetUVs(0, uvs);
            mesh.triangles = triangles.ToArray();

            meshFilter.mesh = mesh;
        }
    }

    public void SetBlock(int brush, int x, int y, int z)
    {
        stage[x + (y * MAX_WIDTH) + (z * MAX_WIDTH * MAX_HEIGHT)] = brush;
    }

    public int GetBlock(int x, int y, int z)
    {
        return stage[x + (y * MAX_WIDTH) + (z * MAX_WIDTH * MAX_HEIGHT)];
    }

    public void SetBufferBlock(int brush, int x, int y, int z)
    {
        stageBuffer[x + (y * MAX_WIDTH) + (z * MAX_WIDTH * MAX_HEIGHT)] = brush;
    }

    public int GetBufferBlock(int x, int y, int z)
    {
        return stageBuffer[x + (y * MAX_WIDTH) + (z * MAX_WIDTH * MAX_HEIGHT)];
    }

    public void RemoveBlocksOutsideWidth()
    {
        for (int i = width; i < MAX_WIDTH; i++)
        {
            for (int j = 0; j < MAX_HEIGHT; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    SetBlock(0, i, j, k);
                }
            }
        }
    }

    public void RemoveBlocksOutsideDepth()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < MAX_HEIGHT; j++)
            {
                for (int k = depth; k < MAX_DEPTH; k++)
                {
                    SetBlock(0, i, j, k);
                }
            }
        }
    }

    public void ShiftBlocks(int shiftX, int shiftY, int shiftZ)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j =  0; j < height; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    int oldPositionX = i - shiftX;
                    int oldPositionY = j - shiftY;
                    int oldPositionZ = k - shiftZ;

                    if (oldPositionX >= 0 && oldPositionX < width
                        && oldPositionY >= 0 && oldPositionY < height
                        && oldPositionZ >= 0 && oldPositionZ < depth)
                    {
                        SetBufferBlock(GetBlock(oldPositionX, oldPositionY, oldPositionZ), i, j, k);
                    }
                    else
                    {
                        SetBufferBlock(0, i, j, k);
                    }
                }
            }
        }

        stageBuffer.CopyTo(stage, 0);
    }
}
