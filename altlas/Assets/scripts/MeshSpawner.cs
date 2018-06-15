using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner {

    public static GameObject SpawnMeshFromCoords(Vector3[][] topLayer, Vector3[][] bottomLayer, Transform parent)
    {
        var gameObject = new GameObject("MapMesh");
        gameObject.transform.parent = parent;
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        var quadsPerEdge = topLayer.Length - 1;
        var mesh = meshFilter.mesh;
        mesh.Clear();

        mesh.vertices = _getVertices(topLayer, bottomLayer);
        mesh.triangles = _getTriangles(quadsPerEdge);

        return gameObject;
    }

    private static Vector3[] _getVertices(Vector3[][] topLayer, Vector3[][] bottomLayer)
    {
        var vertices = new Vector3[(int)Math.Pow(topLayer.Length, 2) * 2];

        var pos = 0;

        foreach (var line in topLayer)
        {
            Array.Copy(line, 0, vertices, pos, line.Length);
            pos += line.Length;
        }

        foreach (var line in bottomLayer)
        {
            Array.Copy(line, 0, vertices, pos, line.Length);
            pos += line.Length;
        }

        return vertices;
    }

    private static int[] _getTriangles(int quadsPerEdge)
    {
        var verticesPerEdge = quadsPerEdge + 1;
        const int trianglesPerQuad = 2;
        const int numberOfLayers = 2;
        const int numberOfSides = 4;
        const int verticesPerFace = 3;
        var triangles = new int[
            verticesPerFace * (
                (int)Math.Pow(quadsPerEdge, 2) * trianglesPerQuad * numberOfLayers + // tob and bottom face
                quadsPerEdge * trianglesPerQuad * numberOfSides)]; // edges

        // naming hints:
        // front = first layer
        // back = second layer

        var indexOffset = 0;

        // first layer
        for (var y = 0; y < quadsPerEdge; y++)
        {
            for (var x = 0; x < quadsPerEdge; x++)
            {
                triangles[indexOffset++] = _getVertexIndex(0, x, y, quadsPerEdge); // top front left
                triangles[indexOffset++] = _getVertexIndex(0, x, y + 1, quadsPerEdge); // bottom front left
                triangles[indexOffset++] = _getVertexIndex(0, x + 1, y, quadsPerEdge); // top front right
                triangles[indexOffset++] = _getVertexIndex(0, x + 1, y, quadsPerEdge); // top front right
                triangles[indexOffset++] = _getVertexIndex(0, x, y + 1, quadsPerEdge); // bottom front left
                triangles[indexOffset++] = _getVertexIndex(0, x + 1, y + 1, quadsPerEdge); // bottom front right
            }
        }

        // second layer
        for (var y = 0; y < quadsPerEdge; y++)
        {
            for (var x = 0; x < quadsPerEdge; x++)
            {
                triangles[indexOffset++] = _getVertexIndex(1, x, y, quadsPerEdge); // top back left
                triangles[indexOffset++] = _getVertexIndex(1, x + 1, y, quadsPerEdge); // top back right
                triangles[indexOffset++] = _getVertexIndex(1, x, y + 1, quadsPerEdge); // bottom back left
                triangles[indexOffset++] = _getVertexIndex(1, x, y + 1, quadsPerEdge); // bottom back left
                triangles[indexOffset++] = _getVertexIndex(1, x + 1, y, quadsPerEdge); // top back right
                triangles[indexOffset++] = _getVertexIndex(1, x + 1, y + 1, quadsPerEdge); // bottom back right
            }
        }

        for (var x = 0; x < quadsPerEdge; x++)
        {
            // top edge
            triangles[indexOffset++] = _getVertexIndex(0, x, 0, quadsPerEdge); // top front left
            triangles[indexOffset++] = _getVertexIndex(0, x + 1, 0, quadsPerEdge); // top front right
            triangles[indexOffset++] = _getVertexIndex(1, x, 0, quadsPerEdge); // top back left
            triangles[indexOffset++] = _getVertexIndex(1, x, 0, quadsPerEdge); // top back left
            triangles[indexOffset++] = _getVertexIndex(0, x + 1, 0, quadsPerEdge); // top front right
            triangles[indexOffset++] = _getVertexIndex(1, x + 1, 0, quadsPerEdge); // top back right

            // bottom edge
            triangles[indexOffset++] = _getVertexIndex(0, x, quadsPerEdge, quadsPerEdge); // bottom front left
            triangles[indexOffset++] = _getVertexIndex(1, x, quadsPerEdge, quadsPerEdge); // bottom back left
            triangles[indexOffset++] = _getVertexIndex(0, x + 1, quadsPerEdge, quadsPerEdge); // bottom front right
            triangles[indexOffset++] = _getVertexIndex(0, x + 1, quadsPerEdge, quadsPerEdge); // bottom front right
            triangles[indexOffset++] = _getVertexIndex(1, x, quadsPerEdge, quadsPerEdge); // bottom back left
            triangles[indexOffset++] = _getVertexIndex(1, x + 1, quadsPerEdge, quadsPerEdge); // bottom back right
        }

        for (var y = 0; y < quadsPerEdge; y++)
        {
            // left edge
            triangles[indexOffset++] = _getVertexIndex(0, 0, y, quadsPerEdge); // top front left
            triangles[indexOffset++] = _getVertexIndex(1, 0, y, quadsPerEdge); // top back left
            triangles[indexOffset++] = _getVertexIndex(0, 0, y + 1, quadsPerEdge); // bottom front left
            triangles[indexOffset++] = _getVertexIndex(0, 0, y + 1, quadsPerEdge); // bottom front left
            triangles[indexOffset++] = _getVertexIndex(1, 0, y, quadsPerEdge); // top back left
            triangles[indexOffset++] = _getVertexIndex(1, 0, y + 1, quadsPerEdge); // bottom back left

            // right edge
            triangles[indexOffset++] = _getVertexIndex(0, quadsPerEdge, y, quadsPerEdge); // top front right
            triangles[indexOffset++] = _getVertexIndex(0, quadsPerEdge, y + 1, quadsPerEdge); // bottom front right
            triangles[indexOffset++] = _getVertexIndex(1, quadsPerEdge, y, quadsPerEdge); // top back right
            triangles[indexOffset++] = _getVertexIndex(1, quadsPerEdge, y, quadsPerEdge); // top back right
            triangles[indexOffset++] = _getVertexIndex(0, quadsPerEdge, y + 1, quadsPerEdge); // bottom front right
            triangles[indexOffset++] = _getVertexIndex(1, quadsPerEdge, y + 1, quadsPerEdge); // bottom back right
        }

        return triangles;
    }

    private static int _getVertexIndex(int layer, int x, int y, int quadsPerEdge)
    {
        Debug.Log("q" + quadsPerEdge + " l" + layer + " x" + x + " y" + y);
        Debug.Log(layer * (int)Math.Pow(quadsPerEdge + 1, 2) + y * (quadsPerEdge + 1) + x);
        return layer * (int) Math.Pow(quadsPerEdge + 1, 2) + y * (quadsPerEdge + 1) + x;
    }
}
