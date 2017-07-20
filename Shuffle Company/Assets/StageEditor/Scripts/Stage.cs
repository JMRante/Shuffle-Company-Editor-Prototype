using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private const int STAGE_WIDTH = 10;
    private const int STAGE_HEIGHT = 10;
    private const int STAGE_DEPTH = 10;

    private int[,,] stage = new int[STAGE_WIDTH, STAGE_HEIGHT, STAGE_DEPTH];

	void Start ()
    {
        // TEST STAGE VALUES
        for (int i = 0; i < STAGE_WIDTH; i++)
        {
            for (int j = 0; j < STAGE_HEIGHT; j++)
            {
                for (int k = 0; k < STAGE_DEPTH; k++)
                {
                    if (i % 2 == 0)
                    {
                        stage[i, 0, k] = 1;
                    }
                    else
                    {
                        stage[i, 0, k] = 0;
                    }
                }
            }
        }

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
            List<Vector2> uvs = new List<Vector2>();
            List<int> triangles = new List<int>();

            // Build stage mesh
            Vector3 vertex;
            Vector3 normal;
            Vector2 uv;
            int triangle = 0;

            // Loop through stage blocks
            for (int i = 0; i < STAGE_WIDTH; i++)
            {
                for (int j = 0; j < STAGE_HEIGHT; j++)
                {
                    for (int k = 0; k < STAGE_DEPTH; k++)
                    {
                        // If the grid space is not empty, build!
                        if (stage[i, j, k] != 0)
                        {
                            // Build block east side
                            if (i + 1 >= STAGE_WIDTH || stage[i + 1, j, k] == 0)
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
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
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

                            // Build block west side
                            if (i - 1 <= -1 || stage[i - 1, j, k] == 0)
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
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 0.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
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

                            // Build block top side
                            if (j + 1 >= STAGE_HEIGHT || stage[i, j + 1, k] == 0)
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
                                uvs.Add(uv);

                                // 2nd Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 0.0f;
                                uvs.Add(uv);

                                // 3rd Vertex
                                vertex.x = i + 0.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 0.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 1.0f;
                                uv.y = 0.0f;
                                uvs.Add(uv);

                                // 4th Vertex
                                vertex.x = i + 1.0f;
                                vertex.y = j + 1.0f;
                                vertex.z = k + 1.0f;
                                vertices.Add(vertex);

                                normals.Add(normal);

                                uv.x = 0.0f;
                                uv.y = 1.0f;
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
                            if (k - 1 <= -1 || stage[i, j, k - 1] == 0)
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
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();

            meshFilter.mesh = mesh;
        }
    }
}
